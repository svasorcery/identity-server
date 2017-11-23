using IdentityModel;
using System.Security.Claims;
using System.Collections.Generic;

namespace Fiery.Api.Identity.Configurations
{
    public class AppUser
    {
        public string SubjectId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Claim> Claims { get; set; }
    }

    public static class Users
    {
        public static List<AppUser> Get()
        {
            return new List<AppUser>
            {
                new AppUser
                {
                    SubjectId = "1",
                    UserName = "svasorcery",
                    Password = "Gfhjkm!23",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "svasorcery"),
                        new Claim(JwtClaimTypes.GivenName, "Vladimir"),
                        new Claim(JwtClaimTypes.FamilyName, "Sinyavsky"),
                        new Claim(JwtClaimTypes.Email, "sva.sorcery@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://github.com/svasorcery/"),
                    }
                },
                new AppUser
                {
                    SubjectId = "2",
                    UserName = "arrantarra",
                    Password = "Gfhjkm!23",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "arrantarra"),
                        new Claim(JwtClaimTypes.Email, "arrantarra@gmail.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "https://github.com/arrantarra/"),
                    }
                }
            };
        }
    }
}
