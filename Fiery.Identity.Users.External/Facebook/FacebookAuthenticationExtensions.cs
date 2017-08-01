using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FacebookAuthenticationExtensions
    {
        public static IApplicationBuilder UseFacebookAuthentication(this IApplicationBuilder app, IConfiguration options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            app.UseFacebookAuthentication(new FacebookOptions
            {
                DisplayName = options.GetSection("Name").Value,
                ClientId = options.GetSection("ClientId").Value,
                ClientSecret = options.GetSection("ClientSecret").Value,
                AuthenticationScheme = FacebookDefaults.AuthenticationScheme,
                SignInScheme = "idsrv.external"
            });

            return app;
        }
    }
}
