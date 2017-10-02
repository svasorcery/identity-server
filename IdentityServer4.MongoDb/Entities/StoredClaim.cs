namespace IdentityServer4.MongoDb.Entities
{
    public class StoredClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }


        public StoredClaim()
        {
        }

        public StoredClaim(string value)
        {
            Type = "userdata";
            Value = value;
        }

        public StoredClaim(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
