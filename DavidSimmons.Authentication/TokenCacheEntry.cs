using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSimmons.Authentication
{
    public class TokenCacheEntry
    {
        public int TokenCacheEntryID { get; set; }
        public string userObjId { get; set; }
        public byte[] cacheBits { get; set; }
        public DateTime LastWrite { get; set; }
    }
}
