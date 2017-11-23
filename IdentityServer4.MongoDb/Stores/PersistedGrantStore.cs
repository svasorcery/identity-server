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
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly IPersistedGrantDbContext _repository;
        private readonly ILogger<PersistedGrantStore> _logger;

        public PersistedGrantStore(
            IPersistedGrantDbContext context,
            ILogger<PersistedGrantStore> logger
            )
        {
            _repository = context;
            _logger = logger;
        }


        public Task StoreAsync(PersistedGrant token)
        {
            try
            {
                var existing = _repository.AllAsync.SingleOrDefault(x => x.Key == token.Key);
                if (existing == null)
                {
                    _logger.LogDebug($"{token.Key} not found in database");

                    var persistedGrant = token.ToEntity();
                    _repository.AddAsync(persistedGrant);
                }
                else
                {
                    _logger.LogDebug($"{token.Key} found in database");

                    existing = token.ToEntity();
                    _repository.UpdateAsync(x => x.Key == token.Key, existing);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "Exception storing persisted grant");
            }

            return Task.FromResult(0);
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            var persistedGrant = _repository.AllAsync.FirstOrDefault(x => x.Key == key);
            var model = persistedGrant.ToModel();

            _logger.LogDebug($"{key} found in database: {model != null}");

            return Task.FromResult(model);
        }

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            var persistedGrants = _repository.AllAsync.Where(x => x.SubjectId == subjectId).ToList();
            var model = persistedGrants.Select(x => x.ToModel());

            _logger.LogDebug($"{persistedGrants.Count} persisted grants found for {subjectId}");

            return Task.FromResult(model);
        }

        public Task RemoveAsync(string key)
        {
            _logger.LogDebug($"removing {key} persisted grant from database");

            _repository.RemoveAsync(x => x.Key == key);

            return Task.FromResult(0);
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            _logger.LogDebug($"removing persisted grants from database for subject \"{subjectId}\", clientId \"{clientId}\"");

            _repository.RemoveAsync(x => x.SubjectId == subjectId && x.ClientId == clientId);

            return Task.FromResult(0);
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            _logger.LogDebug($"removing persisted grants from database for subject \"{subjectId}\", clientId \"{clientId}\", grantType \"{type}\"");

            _repository.RemoveAsync(
               x =>
               x.SubjectId == subjectId &&
               x.ClientId == clientId &&
               x.Type == type);

            return Task.FromResult(0);
        }
    }
}
