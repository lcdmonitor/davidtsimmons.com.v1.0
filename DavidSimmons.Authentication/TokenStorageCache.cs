using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSimmons.Authentication
{
    public class TokenStorageCache : TokenCache
    {
        string userObjId;
        TokenCacheEntry Cache;

        public TokenStorageCache(string userObjectId)
        {
            // associate the cache to the current user of the web app
            userObjId = userObjectId;

            this.AfterAccess = AfterAccessNotification;
            this.BeforeAccess = BeforeAccessNotification;
            this.BeforeWrite = BeforeWriteNotification;

            // look up the entry in the DB
            Cache = CheatTokenCacheStorage.GetTokenCache(userObjId);
            // place the entry in memory
            this.Deserialize((Cache == null) ? null : Cache.cacheBits);
        }

        // clean the db of all tokens associated with the user.
        public override void Clear()
        {
            base.Clear();

            CheatTokenCacheStorage.ClearTokenCache(userObjId);
        }

        // Notification raised before ADAL accesses the cache.
        // This is your chance to update the in-memory copy from the DB, if the in-memory version is stale
        void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            if (Cache == null)
            {
                // first time access
                Cache = CheatTokenCacheStorage.GetTokenCache(userObjId);
            }
            else
            {
                // retrieve last write from the DB
                var dbCache = CheatTokenCacheStorage.GetTokenCache(userObjId);

                // if the in-memory copy is older than the persistent copy, update the in-memory copy
                if (dbCache.LastWrite > Cache.LastWrite)
                    Cache = dbCache;
            }
            this.Deserialize((Cache == null) ? null : Cache.cacheBits);
        }
        // Notification raised after ADAL accessed the cache.
        // If the HasStateChanged flag is set, ADAL changed the content of the cache
        void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if state changed
            if (this.HasStateChanged)
            {
                // retrieve last write from the DB
                Cache = CheatTokenCacheStorage.GetTokenCache(userObjId);

                if (Cache == null)
                {
                    Cache = new TokenCacheEntry
                    {
                        userObjId = userObjId,
                    };
                }
                Cache.LastWrite = DateTime.Now;
                Cache.cacheBits = this.Serialize();

                CheatTokenCacheStorage.StoreTokenCache(Cache);

                this.HasStateChanged = false;
            }
        }
        void BeforeWriteNotification(TokenCacheNotificationArgs args)
        {
            // if you want to ensure that no concurrent write take place, use this notification to place a lock on the entry
        }
    }
}
