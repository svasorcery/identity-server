using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;
using IdentityModel;

namespace Fiery.Api.Identity.Configurations
{
    public static class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "svasorcery",
                    Password = "gfhjkm123",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Svasorcery"),
                        new Claim(JwtClaimTypes.Email, "sva.sorcery@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://github.com/svasorcery/"),
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "arrantarra",
                    Password = "gfhjkm123",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Arrantarra"),
                        new Claim(JwtClaimTypes.Email, "arrantarra@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://github.com/arrantarra/"),
                    }
                }
            };
        }
    }
}
