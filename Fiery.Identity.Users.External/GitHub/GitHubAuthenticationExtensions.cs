using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GitHubAuthenticationExtensions
    {
        public static IApplicationBuilder UseGitHubAuthentication(this IApplicationBuilder app, IConfiguration options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            app.UseGitHubAuthentication(new GitHubAuthenticationOptions
            {
                DisplayName = options.GetSection("Name").Value,
                ClientId = options.GetSection("ClientId").Value,
                ClientSecret = options.GetSection("ClientSecret").Value,
                AuthenticationScheme = GitHubAuthenticationDefaults.AuthenticationScheme,
                SignInScheme = "idsrv.external"
            });

            return app;
        }
    }
}
