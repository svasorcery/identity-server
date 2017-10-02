using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityServer4.MongoDb.Entities;
using MongoDB.Driver;

namespace IdentityServer4.MongoDb.Abstractions
{
    public interface IPersistedGrantDbContext
    {
        IQueryable<StoredPersistedGrant> AllAsync { get; }
        Task<StoredPersistedGrant> FindAsync(Expression<Func<StoredPersistedGrant, bool>> filter);
        Task AddAsync(StoredPersistedGrant entity);
        Task<ReplaceOneResult> UpdateAsync(Expression<Func<StoredPersistedGrant, bool>> filter, StoredPersistedGrant entity);
        Task<DeleteResult> RemoveAsync(Expression<Func<StoredPersistedGrant, bool>> filter);
        Task<DeleteResult> RemoveExpiredAsync();
    }
}
