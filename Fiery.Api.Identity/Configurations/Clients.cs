using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using System.Security.Claims;

namespace Fiery.Api.Identity.Configurations
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                // client credentials grant client
                new Client
                {
                    ClientId = "client.cc",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("fiery_secret".Sha256())
                    },
                    AllowedScopes = { "resources" }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "client.ro",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("fiery_secret".Sha256())
                    },
                    AllowedScopes = { "resources" }
                },

                // implicit password grant client
                new Client
                {
                    ClientId = "client.mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("fiery_secret".Sha256())
                    },
                    
                    RedirectUris = { "http://localhost:50301/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:50301/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "resources"
                    },
                    AllowOfflineAccess = true
                },

                new Client
                {
                    ClientId = "client.js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:50302/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:50302/index.html" },
                    AllowedCorsOrigins ={ "http://localhost:50302" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "resources"
                    }
                }
            };
        }
    }
}
