using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using IdentityServer4.MongoDb.Abstractions;
using IdentityServer4.MongoDb.Configurations;
using IdentityServer4.MongoDb.Entities;
using MongoDB.Driver;

namespace IdentityServer4.MongoDb.DbContexts
{
    public class ScopeDbContext : MongoDbContextBase, IScopeDbContext
    {
        private IMongoCollection<StoredScope> _scope;

        public ScopeDbContext(IOptions<MongoDbRepositoryConfiguration> settings)
            : base(settings)
        {
            _scope = Database.GetCollection<StoredScope>(
                settings.Value.ApiResourceScopeCollectionName);
        }


        public IQueryable<StoredScope> Scopes
        {
            get { return _scope.AsQueryable(); }
        }

        public Task<StoredScope> FindOneAsync(Expression<Func<StoredScope, bool>> filter)
        {
            return _scope.Find(filter).FirstOrDefaultAsync();
        }

        public Task<UpdateResult> StoreAsync(StoredScope entity)
        {
            Expression<Func<StoredScope, bool>> filter = x => x.Name == entity.Name;

            var update = Builders<StoredScope>.Update
                //.Set(x => x.Id, entity.Id)
                .Set(x => x.Name, entity.Name)
                .Set(x => x.DisplayName, entity.DisplayName)
                .Set(x => x.Description, entity.Description)
                .Set(x => x.Required, entity.Required)
                .Set(x => x.Emphasize, entity.Emphasize)
                .Set(x => x.ShowInDiscoveryDocument, entity.ShowInDiscoveryDocument)
                .Set(x => x.UserClaims, entity.UserClaims);

            return _scope.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public Task<DeleteResult> DeleteAsync(Expression<Func<StoredScope, bool>> filter)
        {
            return _scope.DeleteOneAsync(filter);
        }
    }
}
