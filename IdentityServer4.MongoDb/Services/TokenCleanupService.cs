using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using IdentityServer4.MongoDb.DbContexts;
using IdentityServer4.MongoDb.Abstractions;
using IdentityServer4.MongoDb.Configurations;

namespace IdentityServer4.MongoDb.Services
{
    public class TokenCleanupService
    {
        private readonly IOptions<MongoDbRepositoryConfiguration> options;
        private readonly TimeSpan interval;
        private CancellationTokenSource source;

        public TokenCleanupService(IOptions<MongoDbRepositoryConfiguration> options, int interval = 60)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            if (interval < 1) throw new ArgumentException("Interval must be more than 1 second");
            this.options = options;

            this.interval = TimeSpan.FromSeconds(interval);
        }

        public void Start()
        {
            if (source != null) throw new InvalidOperationException("Already started. Call Stop first.");

            source = new CancellationTokenSource();
            Task.Factory.StartNew(() => Start(source.Token));
        }

        public void Stop()
        {
            if (source == null) throw new InvalidOperationException("Not started. Call Start first.");

            source.Cancel();
            source = null;
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    //Logger.Info("CancellationRequested");
                    break;
                }

                try
                {
                    await Task.Delay(interval, cancellationToken);
                }
                catch
                {
                    //Logger.Info("Task.Delay exception. exiting.");
                    break;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    //Logger.Info("CancellationRequested");
                    break;
                }

                await ClearTokens();
            }
        }

        protected virtual IPersistedGrantDbContext CreateOperationalDbContext()
        {
            return new PersistedGrantDbContext(options);
        }

        private async Task ClearTokens()
        {
            try
            {
                //Logger.Info("Clearing tokens");

                var context = CreateOperationalDbContext();

                await context.RemoveExpiredAsync();

            }
            catch (Exception)
            {
                // TODO
                //Logger.ErrorException("Exception cleaning tokens", exception);
            }
        }
    }
}
