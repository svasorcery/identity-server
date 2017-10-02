using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GoogleAuthenticationExtensions
    {
        public static AuthenticationBuilder AddGoogle(this AuthenticationBuilder builder, IConfiguration options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            builder.AddGoogle(GoogleDefaults.AuthenticationScheme, options.GetSection("Name").Value, o =>
            {
                o.ClientId = options.GetSection("ClientId").Value;
                o.ClientSecret = options.GetSection("ClientSecret").Value;
                o.SignInScheme = "idsrv.external";
            });

            return builder;
        }
    }
}
