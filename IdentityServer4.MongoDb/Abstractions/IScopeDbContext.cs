using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityServer4.MongoDb.Entities;
using MongoDB.Driver;

namespace IdentityServer4.MongoDb.Abstractions
{
    public interface IScopeDbContext
    {
        IQueryable<StoredScope> Scopes { get; }
        Task<StoredScope> FindOneAsync(Expression<Func<StoredScope, bool>> filter);
        Task<UpdateResult> StoreAsync(StoredScope entity);
        Task<DeleteResult> DeleteAsync(Expression<Func<StoredScope, bool>> filter);
    }
}
