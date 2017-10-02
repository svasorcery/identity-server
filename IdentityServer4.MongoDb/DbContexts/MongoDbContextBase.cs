using System;
using Microsoft.Extensions.Options;
using IdentityServer4.MongoDb.Configurations;
using MongoDB.Driver;

namespace IdentityServer4.MongoDb.DbContexts
{
    public class MongoDbContextBase
    {
        private readonly IMongoDatabase _database;

        public MongoDbContextBase(IOptions<MongoDbRepositoryConfiguration> settings)
        {
            if (settings.Value == null)
                throw new ArgumentNullException(nameof(settings), "StoreRepositoryConfiguration cannot be null.");

            if (settings.Value.ConnectionString == null)
                throw new ArgumentNullException(nameof(settings), "StoreRepositoryConfiguration.ConnectionString cannot be null.");

            if (settings.Value.DatabaseName == null)
                throw new ArgumentNullException(nameof(settings), "StoreRepositoryConfiguration.DatabaseName cannot be null.");

            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        protected IMongoDatabase Database { get { return _database; } }
    }
}
