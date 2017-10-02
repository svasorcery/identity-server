using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using IdentityServer4.MongoDb.Entities;
using IdentityServer4.MongoDb.Abstractions;
using IdentityServer4.MongoDb.Configurations;
using MongoDB.Driver;

namespace IdentityServer4.MongoDb.DbContexts
{
    public class PersistedGrantDbContext : MongoDbContextBase, IPersistedGrantDbContext
    {
        private IMongoCollection<StoredPersistedGrant> _persistedGrants;

        public PersistedGrantDbContext(IOptions<MongoDbRepositoryConfiguration> settings)
            : base(settings)
        {
            _persistedGrants = Database.GetCollection<StoredPersistedGrant>(
                settings.Value.PersistedGrantCollectionName);
        }


        public IQueryable<StoredPersistedGrant> AllAsync
        {
            get { return _persistedGrants.AsQueryable(); }
        }

        public Task<StoredPersistedGrant> FindAsync(Expression<Func<StoredPersistedGrant, bool>> filter)
        {
            return _persistedGrants.Find(filter).FirstOrDefaultAsync();
        }

        public Task<ReplaceOneResult> UpdateAsync(Expression<Func<StoredPersistedGrant, bool>> filter, StoredPersistedGrant entity)
        {
            return _persistedGrants.ReplaceOneAsync(filter, entity);
        }

        public Task AddAsync(StoredPersistedGrant entity)
        {
            return _persistedGrants.InsertOneAsync(entity);
        }

        public Task<DeleteResult> RemoveAsync(Expression<Func<StoredPersistedGrant, bool>> filter)
        {
            return _persistedGrants.DeleteManyAsync(filter);
        }

        public Task<DeleteResult> RemoveExpiredAsync()
        {
            return RemoveAsync(x => x.Expiration < DateTime.UtcNow);
        }
    }
}
