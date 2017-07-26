﻿using System.Collections.Generic;
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
                }
            };
        }
    }
}