using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace DavidSimmons.Authentication
{
    public class ClaimHelper
    {
        public static string GetCurrentUserName()
        {
            return GetClaimFullName(ClaimsPrincipal.Current.Identity as ClaimsIdentity);
        }

        public static string GetClaimFullName(ClaimsIdentity identity)
        {
            if(identity == null)
            {
                return string.Empty;
            }

            string firstName = identity.FindFirst(Globals.GivennameClaimType).Value;
            string lastName = identity.FindFirst(Globals.SurnameClaimType).Value;

            return string.Concat(firstName, " ", lastName);
        }

        public static async Task<List<string>> GetGroups(ClaimsIdentity claimsId)
        {
            if (claimsId.FindFirst("_claim_names") != null
                && (Json.Decode(claimsId.FindFirst("_claim_names").Value)).groups != null)
                return await GetGroupsFromGraphAPI(claimsId);

            return claimsId.FindAll("groups").Select(c => c.Value).ToList();
        }

        private static async Task<List<string>> GetGroupsFromGraphAPI(ClaimsIdentity claimsIdentity)
        {
            List<string> groupObjectIds = new List<string>();

            // Acquire the Access Token
            ClientCredential credential = new ClientCredential(ConfigHelper.ClientId, ConfigHelper.AppKey);
            AuthenticationContext authContext = new AuthenticationContext(ConfigHelper.Authority,
                new TokenStorageCache(claimsIdentity.FindFirst(Globals.ObjectIdClaimType).Value));
            AuthenticationResult result = authContext.AcquireTokenSilent(ConfigHelper.GraphResourceId, credential,
                new UserIdentifier(claimsIdentity.FindFirst(Globals.ObjectIdClaimType).Value, UserIdentifierType.UniqueId));

            // Get the GraphAPI Group Endpoint for the specific user from the _claim_sources claim in token
            string groupsClaimSourceIndex = (Json.Decode(claimsIdentity.FindFirst("_claim_names").Value)).groups;
            var groupClaimsSource = (Json.Decode(claimsIdentity.FindFirst("_claim_sources").Value))[groupsClaimSourceIndex];
            string requestUrl = groupClaimsSource.endpoint + "?api-version=" + ConfigHelper.GraphApiVersion;

            // Prepare and Make the POST request
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            StringContent content = new StringContent("{\"securityEnabledOnly\": \"false\"}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            HttpResponseMessage response = await client.SendAsync(request);

            // Endpoint returns JSON with an array of Group ObjectIDs
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var groupsResult = (Json.Decode(responseContent)).value;

                foreach (string groupObjectID in groupsResult)
                    groupObjectIds.Add(groupObjectID);
            }
            else
            {
                throw new WebException();
            }

            return groupObjectIds;
        }
    }
}
