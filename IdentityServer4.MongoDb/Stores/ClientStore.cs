using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using IdentityServer4.MongoDb.Mappers;
using IdentityServer4.MongoDb.Abstractions;

namespace IdentityServer4.MongoDb.Stores
{
    public class ClientStore : IClientStore
    {
        private readonly IClientDbContext _repository;
        private readonly ILogger<ClientStore> _logger;

        public ClientStore(
            IClientDbContext context,
            ILogger<ClientStore> logger
            )
        {
            _repository = context;
            _logger = logger;
        }


        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = _repository.AllAsync.FirstOrDefault(x => x.ClientId == clientId && x.Enabled);
            var model = client?.ToModel();

            _logger.LogDebug($"Client \"{clientId}\" found in database: {model != null}");

            return Task.FromResult(model);
        }
    }
}
