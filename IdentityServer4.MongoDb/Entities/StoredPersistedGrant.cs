using IdentityServer4.Models;
using MongoDB.Bson;

namespace IdentityServer4.MongoDb.Entities
{
    public class StoredPersistedGrant : PersistedGrant
    {
        public ObjectId Id { get; set; }


        public StoredPersistedGrant()
        {
        }
    }
}
