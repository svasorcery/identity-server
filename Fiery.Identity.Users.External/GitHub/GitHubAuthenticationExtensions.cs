using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GitHubAuthenticationExtensions
    {
        public static AuthenticationBuilder AddGitHub(this AuthenticationBuilder builder, IConfiguration options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            builder.AddGitHub(GitHubAuthenticationDefaults.AuthenticationScheme, options.GetSection("Name").Value, o =>
            {
                o.ClientId = options.GetSection("ClientId").Value;
                o.ClientSecret = options.GetSection("ClientSecret").Value;
                o.SignInScheme = "idsrv.external";
            });

            return builder;
        }
    }
}
