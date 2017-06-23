using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Fiery.Identity.Clients.ResourceOwner
{
    class Program
    {
        static void Main(string[] args)
        {
            // discover endpoints from metadata
            var disco = DiscoveryClient.GetAsync("http://localhost:50100").Result;

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client.ro", "fiery_secret");
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("svasorcery", "gfhjkm123", "resources").Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
        }
    }
}