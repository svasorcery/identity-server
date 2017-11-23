namespace IdentityServer4.MongoDb.Configurations
{
    public class MongoDbRepositoryConfiguration
    {
        // database
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        // collections
        public string UserProfileCollectionName { get; set; }
        public string ClientCollectionName { get; set; }
        public string IdentityResourceCollectionName { get; set; }
        public string ApiResourceCollectionName { get; set; }
        public string ApiResourceScopeCollectionName { get; set; }
        public string PersistedGrantCollectionName { get; set; }
    }
}
