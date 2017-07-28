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
using Microsoft.Extensions.Configuration;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using AspNet.Security.OAuth.GitHub;
using AspNet.Security.OAuth.Vkontakte;

namespace Fiery.Api.Identity
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            //if (env.IsDevelopment())
            //{
            //    builder.AddUserSecrets<Startup>();
            //}

            Configuration = builder.Build();
        }

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

            // TODO: move options to separate library
            app.UseGoogleAuthentication(new GoogleOptions
            {
                DisplayName = Configuration["Authentication:ExternalProviders:Google:Name"],
                ClientId = Configuration["Authentication:ExternalProviders:Google:ClientId"],
                ClientSecret = Configuration["Authentication:ExternalProviders:Google:ClientSecret"],
                AuthenticationScheme = GoogleDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme
            });
            app.UseMicrosoftAccountAuthentication(new MicrosoftAccountOptions
            {
                DisplayName = Configuration["Authentication:ExternalProviders:Microsoft:Name"],
                ClientId = Configuration["Authentication:ExternalProviders:Microsoft:ClientId"],
                ClientSecret = Configuration["Authentication:ExternalProviders:Microsoft:ClientSecret"],
                AuthenticationScheme = MicrosoftAccountDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme
            });
            app.UseTwitterAuthentication(new TwitterOptions
            {
                DisplayName = Configuration["Authentication:ExternalProviders:Twitter:Name"],
                ConsumerKey = Configuration["Authentication:ExternalProviders:Twitter:ClientId"],
                ConsumerSecret = Configuration["Authentication:ExternalProviders:Twitter:ClientSecret"],
                AuthenticationScheme = TwitterDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme
            });
            app.UseFacebookAuthentication(new FacebookOptions
            {
                DisplayName = Configuration["Authentication:ExternalProviders:Facebook:Name"],
                ClientId = Configuration["Authentication:ExternalProviders:Facebook:ClientId"],
                ClientSecret = Configuration["Authentication:ExternalProviders:Facebook:ClientSecret"],
                AuthenticationScheme = FacebookDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme
            });
            app.UseGitHubAuthentication(new GitHubAuthenticationOptions
            {
                DisplayName = Configuration["Authentication:ExternalProviders:GitHub:Name"],
                ClientId = Configuration["Authentication:ExternalProviders:GitHub:ClientId"],
                ClientSecret = Configuration["Authentication:ExternalProviders:GitHub:ClientSecret"],
                AuthenticationScheme = GitHubAuthenticationDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme
            });
            app.UseVkontakteAuthentication(new VkontakteAuthenticationOptions
            {
                DisplayName = Configuration["Authentication:ExternalProviders:VK:Name"],
                ClientId = Configuration["Authentication:ExternalProviders:VK:ClientId"],
                ClientSecret = Configuration["Authentication:ExternalProviders:VK:ClientSecret"],
                AuthenticationScheme = VkontakteAuthenticationDefaults.AuthenticationScheme,
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme
            });

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
