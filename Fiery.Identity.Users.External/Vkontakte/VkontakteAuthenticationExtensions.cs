using AspNet.Security.OAuth.Vkontakte;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
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

            /*builder.AddVkontakte(new VkontakteAuthenticationOptions
            {
                DisplayName = options.GetSection("Name").Value,
                ClientId = options.GetSection("ClientId").Value,
                ClientSecret = options.GetSection("ClientSecret").Value,
                AuthenticationScheme = VkontakteAuthenticationDefaults.AuthenticationScheme,
                SignInScheme = "idsrv.external"
            });*/

            return builder;
        }
    }
}
