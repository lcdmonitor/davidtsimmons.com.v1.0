using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSimmons.Authentication
{
    public static class Globals
    {
        private static string objectIdClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        private const string tenantIdClaimType = "http://schemas.microsoft.com/identity/claims/tenantid";
        private const string surnameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
        private const string givennameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        private static List<String> roles = new List<String>(new String[4] { "Admin", "Observer", "Writer", "Approver" });
        
        public  static string ObjectIdClaimType { get { return objectIdClaimType; } }
        public  static string TenantIdClaimType { get { return tenantIdClaimType; } }
        public  static string SurnameClaimType { get { return surnameClaimType; } }
        public static string GivennameClaimType { get { return givennameClaimType; } }
        //TODO: Roles Collection not Realle used right not but could be :)
        public static List<String> Roles { get { return roles; } }
    }
}
