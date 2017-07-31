using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExternalAuthenticationExtensions
    {
        public static IApplicationBuilder UseExternalAuthentication(this IApplicationBuilder app, IConfiguration options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            app.UseGoogleAuthentication(options.GetSection("Google"));
            app.UseMicrosoftAccountAuthentication(options.GetSection("Microsoft"));
            app.UseTwitterAuthentication(options.GetSection("Twitter"));
            app.UseFacebookAuthentication(options.GetSection("Facebook"));
            app.UseGitHubAuthentication(options.GetSection("GitHub"));
            app.UseVkontakteAuthentication(options.GetSection("VK"));

            return app;
        }
    }
}
