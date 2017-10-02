using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using IdentityServer4.MongoDb.Entities;
using MongoDB.Driver;

namespace IdentityServer4.MongoDb.Abstractions
{
    public interface IResourceDbContext
    {
        IQueryable<StoredIdentityResource> IdentityResources { get; }
        Task<StoredIdentityResource> FindIdentityAsync(Expression<Func<StoredIdentityResource, bool>> filter);
        Task<UpdateResult> StoreIdentityAsync(StoredIdentityResource entity);
        Task<DeleteResult> DeleteIdentityAsync(Expression<Func<StoredIdentityResource, bool>> filter);

        IQueryable<StoredApiResource> ApiResources { get; }
        Task<StoredApiResource> FindApiAsync(Expression<Func<StoredApiResource, bool>> filter);
        Task<UpdateResult> StoreApiAsync(StoredApiResource entity);
        Task<UpdateResult> StoreApiAsync(StoredApiResource entity, ICollection<StoredScope> scopes);
        Task<DeleteResult> DeleteApiAsync(Expression<Func<StoredApiResource, bool>> filter);
    }
}
