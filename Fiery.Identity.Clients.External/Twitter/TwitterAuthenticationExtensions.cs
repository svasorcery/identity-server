using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TwitterAuthenticationExtensions
    {
        public static IApplicationBuilder UseTwitterAuthentication(this IApplicationBuilder app, IConfiguration options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            app.UseTwitterAuthentication(new TwitterOptions
            {
                DisplayName = options.GetSection("Name").Value,
                ConsumerKey = options.GetSection("ClientId").Value,
                ConsumerSecret = options.GetSection("ClientSecret").Value,
                AuthenticationScheme = TwitterDefaults.AuthenticationScheme,
                SignInScheme = "idsrv.external"
            });

            return app;
        }
    }
}
