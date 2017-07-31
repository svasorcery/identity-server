using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoogleAuthenticationExtensions
    {
        public static IApplicationBuilder UseGoogleAuthentication(this IApplicationBuilder app, IConfiguration options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            app.UseGoogleAuthentication(new GoogleOptions
            {
                DisplayName = options.GetSection("Name").Value,
                ClientId = options.GetSection("ClientId").Value,
                ClientSecret = options.GetSection("ClientSecret").Value,
                AuthenticationScheme = GoogleDefaults.AuthenticationScheme,
                SignInScheme = "idsrv.external"
            });

            return app;
        }
    }
}
