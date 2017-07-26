using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace Fiery.Api.Identity.Configurations
{
    public static class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentity()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApi()
        {
            return new List<ApiResource>
            {
                new ApiResource("resources", "Resources API")
            };
        }

        public static List<Scope> GetApiScopes()
        {
            return new List<Scope>
            {

            };
        }
    }
}
