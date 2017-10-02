using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using IdentityServer4.MongoDb.Abstractions;
using IdentityServer4.MongoDb.Configurations;
using IdentityServer4.MongoDb.Entities;
using MongoDB.Driver;

namespace IdentityServer4.MongoDb.DbContexts
{
    public class ResourceDbContext : MongoDbContextBase, IResourceDbContext
    {
        private IMongoCollection<StoredIdentityResource> _identityResource;
        private IMongoCollection<StoredApiResource> _apiResource;

        public ResourceDbContext(IOptions<MongoDbRepositoryConfiguration> settings)
            : base(settings)
        {
            _identityResource = Database.GetCollection<StoredIdentityResource>(
                settings.Value.IdentityResourceCollectionName);
            _apiResource = Database.GetCollection<StoredApiResource>(
                settings.Value.ApiResourceCollectionName);
        }


        public IQueryable<StoredIdentityResource> IdentityResources
        {
            get { return _identityResource.AsQueryable(); }
        }

        public Task<StoredIdentityResource> FindIdentityAsync(Expression<Func<StoredIdentityResource, bool>> filter)
        {
            return _identityResource.Find(filter).FirstOrDefaultAsync();
        }

        public Task<UpdateResult> StoreIdentityAsync(StoredIdentityResource entity)
        {
            Expression<Func<StoredIdentityResource, bool>> filter = x => x.Name == entity.Name;

            var update = Builders<StoredIdentityResource>.Update
                //.Set(x => x.Id, entity.Id)
                .Set(x => x.Enabled, entity.Enabled)
                .Set(x => x.Name, entity.Name)
                .Set(x => x.DisplayName, entity.DisplayName)
                .Set(x => x.Description, entity.Description)
                .Set(x => x.Required, entity.Required)
                .Set(x => x.Emphasize, entity.Emphasize)
                .Set(x => x.ShowInDiscoveryDocument, entity.ShowInDiscoveryDocument)
                .Set(x => x.UserClaims, entity.UserClaims);

            return _identityResource.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public Task<DeleteResult> DeleteIdentityAsync(Expression<Func<StoredIdentityResource, bool>> filter)
        {
            return _identityResource.DeleteOneAsync(filter);
        }


        public IQueryable<StoredApiResource> ApiResources
        {
            get { return _apiResource.AsQueryable(); }
        }

        public Task<StoredApiResource> FindApiAsync(Expression<Func<StoredApiResource, bool>> filter)
        {
            return _apiResource.Find(filter).FirstOrDefaultAsync();
        }

        public Task<UpdateResult> StoreApiAsync(StoredApiResource entity)
        {
            Expression<Func<StoredApiResource, bool>> filter = x => x.Name == entity.Name;

            var update = Builders<StoredApiResource>.Update
                //.Set(x => x.Id, entity.Id)
                .Set(x => x.Enabled, entity.Enabled)
                .Set(x => x.Name, entity.Name)
                .Set(x => x.DisplayName, entity.DisplayName)
                .Set(x => x.Description, entity.Description)
                .Set(x => x.ApiSecrets, entity.ApiSecrets)
                .Set(x => x.UserClaims, entity.UserClaims)
                .Set(x => x.Scopes, entity.Scopes);

            return _apiResource.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public Task<UpdateResult> StoreApiAsync(StoredApiResource entity, ICollection<StoredScope> scopes)
        {
            Expression<Func<StoredApiResource, bool>> filter = x => x.Name == entity.Name;

            var update = Builders<StoredApiResource>.Update
                //.Set(x => x.Id, entity.Id)
                .Set(x => x.Enabled, entity.Enabled)
                .Set(x => x.Name, entity.Name)
                .Set(x => x.DisplayName, entity.DisplayName)
                .Set(x => x.Description, entity.Description)
                .Set(x => x.ApiSecrets, entity.ApiSecrets)
                .Set(x => x.UserClaims, entity.UserClaims)
                .Set(x => x.Scopes, scopes);

            return _apiResource.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public Task<DeleteResult> DeleteApiAsync(Expression<Func<StoredApiResource, bool>> filter)
        {
            return _apiResource.DeleteOneAsync(filter);
        }
    }
}
