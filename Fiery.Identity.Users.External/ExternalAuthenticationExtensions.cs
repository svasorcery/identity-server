using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExternalAuthenticationExtensions
    {
        public static AuthenticationBuilder AddExternal(this AuthenticationBuilder builder, IConfiguration options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            builder.AddGoogle(options.GetSection("Google"));
            builder.AddMicrosoftAccount(options.GetSection("Microsoft"));
            builder.AddTwitter(options.GetSection("Twitter"));
            builder.AddFacebook(options.GetSection("Facebook"));
            //builder.AddGitHub(options.GetSection("GitHub"));
            //builder.AddVkontakte(options.GetSection("VK"));

            return builder;
        }
    }
}
