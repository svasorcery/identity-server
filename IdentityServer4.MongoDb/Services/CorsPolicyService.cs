using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using IdentityServer4.Services;
using IdentityServer4.MongoDb.Abstractions;

namespace IdentityServer4.MongoDb.Services
{
    public class CorsPolicyService : ICorsPolicyService
    {
        private readonly IClientDbContext _clients;
        private readonly ILogger<CorsPolicyService> _logger;

        public CorsPolicyService(IClientDbContext context, ILogger<CorsPolicyService> logger)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _clients = context;
            _logger = logger;
        }


        public Task<bool> IsOriginAllowedAsync(string origin)
        {
            var distinctOrigins = _clients.AllAsync.ToArray()
                .SelectMany(x => x.AllowedCorsOrigins)
                .Where(x => x != null)
                .Distinct();

            var isAllowed = distinctOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase);

            // log result
            var logMessage = isAllowed ?
                $"Client list checked and origin: {origin} is allowed" :
                $"Client list checked and origin: {origin} is not allowed";
            _logger.LogDebug(logMessage);

            return Task.FromResult(isAllowed);
        }
    }
}
