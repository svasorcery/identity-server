using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using IdentityServer4.MongoDb.Entities;
using MongoDB.Driver;

namespace IdentityServer4.MongoDb.Abstractions
{
    public interface IClientDbContext
    {
        IQueryable<StoredClient> AllAsync { get; }
        Task<StoredClient> FindAsync(Expression<Func<StoredClient, bool>> filter);
        Task AddAsync(StoredClient entity);
        Task<ReplaceOneResult> UpdateAsync(Expression<Func<StoredClient, bool>> filter, StoredClient entity);
        Task<DeleteResult> DeleteAsync(Expression<Func<StoredClient, bool>> filter);
    }
}
