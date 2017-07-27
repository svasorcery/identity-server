using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
                AuthenticationScheme = "Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "986969870699-fkd5cmus27hbcn9ijuhep73cbd6acuhf.apps.googleusercontent.com",
                ClientSecret = "oozJVDLbu5yUcvp-VBALjJMl"
            });
            app.UseFacebookAuthentication(new FacebookOptions
            {
                DisplayName = "Facebook",
                AuthenticationScheme = "Facebook",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "291849607954027",
                ClientSecret = "971f72cf26b226c3122335d3928ce9cf"
            });

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
