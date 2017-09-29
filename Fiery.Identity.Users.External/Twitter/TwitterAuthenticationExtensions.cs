using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class TwitterAuthenticationExtensions
    {
        public static AuthenticationBuilder AddTwitter(this AuthenticationBuilder builder, IConfiguration options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            builder.AddTwitter(TwitterDefaults.AuthenticationScheme, options.GetSection("Name").Value, o =>
            {
                o.ConsumerKey = options.GetSection("ClientId").Value;
                o.ConsumerSecret = options.GetSection("ClientSecret").Value;
                o.SignInScheme = "idsrv.external";
            });

            return builder;
        }
    }
}
