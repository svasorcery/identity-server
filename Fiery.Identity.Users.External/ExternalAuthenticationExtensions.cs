using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExternalIdentityProvidersExtensions
    {
        public static IServiceCollection AddExternalIdentityProviders(this IServiceCollection services, IConfiguration options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            // configures the OpenIdConnect handlers to persist the state parameter into the server-side IDistributedCache.
            services.AddAuthentication()
                .AddGoogle(options.GetSection("Google"))
                .AddMicrosoftAccount(options.GetSection("Microsoft"))
                .AddTwitter(options.GetSection("Twitter"))
                .AddFacebook(options.GetSection("Facebook"));
                //.AddGitHub(options.GetSection("GitHub"))
                //.AddVkontakte(options.GetSection("VK"))

            return services;
        }
    }
}
