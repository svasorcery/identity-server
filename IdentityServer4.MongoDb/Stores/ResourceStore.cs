using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.MongoDb.Mappers;
using IdentityServer4.MongoDb.Abstractions;

namespace IdentityServer4.MongoDb.Stores
{
    public class ResourceStore : IResourceStore
    {
        private readonly IResourceDbContext _repository;
        private readonly ILogger<ResourceStore> _logger;

        public ResourceStore(
            IResourceDbContext context,
            ILogger<ResourceStore> logger
            )
        {
            _repository = context;
            _logger = logger;
        }


        public Task<Resources> GetAllResourcesAsync()
        {
            var identityResources = _repository.IdentityResources.ToArray().Select(x => x.ToModel());
            var apiResources = _repository.ApiResources.ToArray().Select(x => x.ToModel());
            var result = new Resources(identityResources, apiResources);

            return Task.FromResult(result);
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            var apiResource = _repository.ApiResources.FirstOrDefault(x => x.Name == name);

            var model = apiResource.ToModel();

            _logger.LogDebug($"{name} found in database: {model != null}");

            return Task.FromResult(model);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var api = from a in _repository.ApiResources.ToArray()
                      from s in a.Scopes
                      where scopeNames.Contains(s.Name)
                      select a.ToModel();

            _logger.LogDebug($"ApiResources found in database: {api != null}");

            return Task.FromResult(api);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames == null) throw new ArgumentNullException(nameof(scopeNames));

            var identity = from i in _repository.IdentityResources.ToArray()
                           where scopeNames.Contains(i.Name)
                           select i.ToModel();

            _logger.LogDebug($"IdentityResources found in database: {identity != null}");

            return Task.FromResult(identity);
        }
    }
}
