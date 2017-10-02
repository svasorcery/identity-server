using System;

namespace IdentityServer4.MongoDb.Entities
{
    public class StoredSecret //: Secret
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime? Expiration { get; set; }


        public StoredSecret()
        {
        }

        public StoredSecret(
            string type, string value,
            string description = null, DateTime? expiration = null
            )
        {
            Type = type;
            Value = value;
            Description = "";
            Expiration = null;
        }
    }
}
