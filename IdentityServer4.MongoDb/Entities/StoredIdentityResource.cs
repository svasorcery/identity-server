using System.Collections.Generic;
using IdentityServer4.Models;
using MongoDB.Bson;

namespace IdentityServer4.MongoDb.Entities
{
    public class StoredIdentityResource : IdentityResource
    {
        public ObjectId Id { get; set; }


        public StoredIdentityResource()
        {
            Enabled = true;
            Required = false;
            Emphasize = true;
            ShowInDiscoveryDocument = true;
        }

        public StoredIdentityResource(string name)
            : this(name, name)
        {
        }

        public StoredIdentityResource(string name, string displayName)
            : this(name, displayName, new List<string>())
        {
        }

        public StoredIdentityResource(string name, string displayName, ICollection<string> claims)
        {
            Enabled = true;
            Required = true;
            Name = name;
            DisplayName = displayName;
            Description = displayName;
            Emphasize = false;
            ShowInDiscoveryDocument = true;
            UserClaims = claims;
        }
    }
}
