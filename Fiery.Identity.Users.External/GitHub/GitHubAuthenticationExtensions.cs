using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
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

            /*builder.AddGitHub(new GitHubAuthenticationOptions
            {
                DisplayName = options.GetSection("Name").Value,
                ClientId = options.GetSection("ClientId").Value,
                ClientSecret = options.GetSection("ClientSecret").Value,
                AuthenticationScheme = GitHubAuthenticationDefaults.AuthenticationScheme,
                SignInScheme = "idsrv.external"
            });*/

            return builder;
        }
    }
}
