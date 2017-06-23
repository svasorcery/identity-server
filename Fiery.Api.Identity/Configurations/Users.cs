using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

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
                    Password = "gfhjkm123"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "arrantarra",
                    Password = "gfhjkm123"
                }
            };
        }
    }
}
