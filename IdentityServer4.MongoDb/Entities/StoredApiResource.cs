using IdentityServer4.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace IdentityServer4.MongoDb.Entities
{
    public class StoredApiResource //: ApiResource
    {
        public ObjectId Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public ICollection<StoredSecret> ApiSecrets { get; set; }
        public ICollection<string> UserClaims { get; set; }
        public ICollection<StoredScope> Scopes { get; set; }


        public StoredApiResource()
        {
            Enabled = true;
            ApiSecrets = new List<StoredSecret>();
            UserClaims = new List<string>();
            Scopes = new List<StoredScope>();
        }

        public StoredApiResource(
            string name
            ) : this(name, name)
        {
        }

        public StoredApiResource(
            string name,
            string displayName
            ) : this(name, displayName, new List<string>())
        {
        }

        public StoredApiResource(
            string name,
            string displayName,
            ICollection<string> claims
            ) : this(name, displayName, displayName, claims)
        {
        }

        public StoredApiResource(
            string name,
            string displayName,
            string description,
            ICollection<string> userClaims
            )
        {
            Enabled = true;
            Name = name;
            DisplayName = displayName;
            Description = description;
            ApiSecrets = new List<StoredSecret>();
            UserClaims = userClaims;
            Scopes = new List<StoredScope>();
        }
    }
}
