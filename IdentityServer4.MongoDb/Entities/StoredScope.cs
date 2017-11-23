using System.Collections.Generic;
using MongoDB.Bson;
using IdentityServer4.Models;

namespace IdentityServer4.MongoDb.Entities
{
    public class StoredScope //: Scope
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public ICollection<string> UserClaims { get; set; }


        public StoredScope()
        {
            Required = false;
            Emphasize = true;
            ShowInDiscoveryDocument = true;
        }

        public StoredScope(string name)
            : this(name, name)
        {
        }

        public StoredScope(string name, string displayName)
            : this(name, displayName, new List<string>())
        {
        }

        public StoredScope(string name, string displayName, ICollection<string> claims)
            : this(name, displayName, displayName, claims)
        {
        }

        public StoredScope(
            string name,
            string displayName,
            string description,
            ICollection<string> claims)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
            Required = false;
            Emphasize = true;
            ShowInDiscoveryDocument = true;
            UserClaims = claims;
        }
    }
}
