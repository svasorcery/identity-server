using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using IdentityServer4.MongoDb.Entities;
using IdentityServer4.MongoDb.Abstractions;
using IdentityServer4.MongoDb.Configurations;
using MongoDB.Driver;

namespace IdentityServer4.MongoDb.DbContexts
{
    public class ClientDbContext : MongoDbContextBase, IClientDbContext
    {
        private IMongoCollection<StoredClient> _clients;

        public ClientDbContext(IOptions<MongoDbRepositoryConfiguration> settings)
            : base(settings)
        {
            _clients = Database.GetCollection<StoredClient>(
                settings.Value.ClientCollectionName);
        }


        public IQueryable<StoredClient> AllAsync
        {
            get { return _clients.AsQueryable(); }
        }

        public Task<StoredClient> FindAsync(Expression<Func<StoredClient, bool>> filter)
        {
            return _clients.Find(filter).FirstOrDefaultAsync();
        }

        public Task AddAsync(StoredClient entity)
        {
            return _clients.InsertOneAsync(entity);
        }

        public Task<ReplaceOneResult> UpdateAsync(Expression<Func<StoredClient, bool>> filter, StoredClient entity)
        {
            return _clients.ReplaceOneAsync(filter, entity);
        }

        public Task<DeleteResult> DeleteAsync(Expression<Func<StoredClient, bool>> filter)
        {
            return _clients.DeleteOneAsync(filter);
        }
    }
}
