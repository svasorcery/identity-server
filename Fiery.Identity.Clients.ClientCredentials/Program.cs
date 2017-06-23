using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Fiery.Identity.Clients.ClientCredentials
{
    class Program
    {
        static void Main(string[] args)
        {
            // discover endpoints from metadata
            var disco = DiscoveryClient.GetAsync("http://localhost:50100").Result;

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client.cc", "fiery_secret");
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("resources").Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = client.GetAsync("http://localhost:50201/identity").Result;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
