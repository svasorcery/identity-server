using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using AspNet.Security.OAuth.GitHub;

namespace Fiery.Api.Identity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddTestUsers(Configurations.Users.Get())
                .AddInMemoryClients(Configurations.Clients.Get())
                .AddInMemoryApiResources(Configurations.Resources.GetApi())
                .AddInMemoryIdentityResources(Configurations.Resources.GetIdentity());

            // Add Mvc with custom views location
            services.AddMvc()
                .AddRazorOptions(razor => razor.ViewLocationExpanders.Add(new UI.CustomViewLocationExpander()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseIdentityServer();

            // TODO: move options to configuration
            app.UseGoogleAuthentication(new GoogleOptions
            {
                DisplayName = "Google",
                AuthenticationScheme = GoogleDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "986969870699-fkd5cmus27hbcn9ijuhep73cbd6acuhf.apps.googleusercontent.com",
                ClientSecret = "oozJVDLbu5yUcvp-VBALjJMl"
            });
            app.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions
            {
                DisplayName = "Microsoft",
                AuthenticationScheme = MicrosoftAccountDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "556abe8c-6ffe-4bd9-b093-5c55e9e8e077",
                ClientSecret = "nS9JXh7jRMSYYvYVemomKun"
            });
            app.UseTwitterAuthentication(new TwitterOptions
            {
                DisplayName = "Twitter",
                AuthenticationScheme = TwitterDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ConsumerKey = "oG3KAhAEv4pRc017w0gCDKo3S",
                ConsumerSecret = "VR7yBqMG9WsIHJ3YNcFJmQL289mpAyh6DxphE8iLdgmL16BCH1"
            });
            app.UseFacebookAuthentication(new FacebookOptions
            {
                DisplayName = "Facebook",
                AuthenticationScheme = FacebookDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "291849607954027",
                ClientSecret = "971f72cf26b226c3122335d3928ce9cf"
            });
            app.UseGitHubAuthentication(new GitHubAuthenticationOptions
            {
                DisplayName = "GitHub",
                AuthenticationScheme = GitHubAuthenticationDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "Iv1.52c19095558a1a01",
                ClientSecret = "a53df8ef268014527d5718cc043e16bce07753ee",
            });

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
