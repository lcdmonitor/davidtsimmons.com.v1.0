using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSimmons.Authentication
{
    /// <summary>
    /// TODO: THIS IS NEITHER THREAD SAFE NOR CLUSTER OK
    /// NEED TO USE PERSISTENT TABLE STORAGE
    /// INVESTIGATE POSIBILITY OF USING ioc FOR DEPENDENCIES
    /// </summary>
    public static class CheatTokenCacheStorage
    {
        private static Dictionary<string, TokenCacheEntry> _tokenStorage = new Dictionary<string, TokenCacheEntry>();

        public static void StoreTokenCache(TokenCacheEntry c)
        {
            _tokenStorage[c.userObjId] = c;
        }

        public static TokenCacheEntry GetTokenCache(string ID)
        {
            if (_tokenStorage.ContainsKey(ID))
            {
                return _tokenStorage[ID];
            }
            else
            {
                return null;
            }
        }
        
        public static void ClearTokenCache(string ID)
        {
            if (_tokenStorage.ContainsKey(ID))
            {
                _tokenStorage[ID] = null;
            }
        }
    }
}
