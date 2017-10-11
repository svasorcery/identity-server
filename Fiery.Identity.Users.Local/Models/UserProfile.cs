using System.Collections.Generic;
using System.Security.Claims;

namespace Fiery.Identity.Users.Local.Models
{
    public class UserProfile
    {
        public string Subject { get; set; }

        public string ValidatedThrough { get; protected set; }

        public string FullName { get; set; }

        public string EMail { get; set; }

        public IEnumerable<Claim> Claims { get; set; }


        public UserProfile()
        {
            ValidatedThrough = "LocalDB";
        }
    }
}
