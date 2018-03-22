using AspNet.Security.OAuth.Vkontakte;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class VkontakteAuthenticationExtensions
    {
        public static AuthenticationBuilder AddVkontakte(this AuthenticationBuilder builder, IConfiguration options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            builder.AddVkontakte(VkontakteAuthenticationDefaults.AuthenticationScheme, options.GetSection("Name").Value, o =>
            {
                o.ClientId = options.GetSection("ClientId").Value;
                o.ClientSecret = options.GetSection("ClientSecret").Value;
                o.SignInScheme = "idsrv.external";
            });

            return builder;
        }
    }
}
