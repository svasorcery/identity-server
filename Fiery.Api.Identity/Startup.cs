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
using Microsoft.Extensions.Options;
using IdentityServer4.MongoDb.Abstractions;
using IdentityServer4.MongoDb.Mappers;
using IdentityServer4.MongoDb.Services;
using IdentityServer4.MongoDb.Configurations;
using System.Security.Cryptography.X509Certificates;

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
                .AddSigningCredential(new X509Certificate2(Configuration["PrimaryCert"], "T5er41E2bf7im0f1Rt3VW637Hjki083f4G4n4N6d80Rty7uBe0"))
                .AddTestUsers(Configurations.Users.Get())
                .AddConfigurationStore(Configuration.GetSection("Databases:MongoDb"))
                .AddOperationalStore(Configuration.GetSection("Databases:MongoDb"));

            services.AddAuthentication()
                .AddExternal(Configuration.GetSection("Authentication:ExternalProviders"));

            // Add Mvc with custom views location
            services.AddMvc()
                .AddRazorOptions(razor => razor.ViewLocationExpanders.Add(new UI.CustomViewLocationExpander()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // this will do the initial DB population
            InitializeDatabase(app);

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

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }


        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // Seed Clients
                var clientDbContext = serviceScope.ServiceProvider.GetService<IClientDbContext>();
                if (!clientDbContext.AllAsync.Any())
                {
                    var clients = Configurations.Clients.Get();
                    foreach (var client in clients)
                    {
                        clientDbContext.AddAsync(client.ToEntity());
                    }
                }

                // Seed Resources
                var resourceDbContext = serviceScope.ServiceProvider.GetService<IResourceDbContext>();
                // Identity Resources
                if (!resourceDbContext.IdentityResources.Any())
                {
                    var identities = Configurations.Resources.GetIdentity();
                    foreach (var identity in identities)
                    {
                        resourceDbContext.StoreIdentityAsync(identity.ToEntity());
                    }
                }
                // API Resources
                if (!resourceDbContext.ApiResources.Any())
                {
                    var apis = Configurations.Resources.GetApi();
                    foreach (var api in apis)
                    {
                        resourceDbContext.StoreApiAsync(api.ToEntity());
                    }
                }

                // Seed API Resource Scopes
                var scopeDbContext = serviceScope.ServiceProvider.GetService<IScopeDbContext>();
                if (!scopeDbContext.Scopes.Any())
                {
                    var scopes = Configurations.Resources.GetApiScopes();
                    foreach (var scope in scopes)
                    {
                        scopeDbContext.StoreAsync(scope.ToEntity());
                    }
                }

                // Token expired cleanup
                var options = serviceScope.ServiceProvider.GetService<IOptions<MongoDbRepositoryConfiguration>>();
                var tokenCleanup = new TokenCleanupService(options);
                tokenCleanup.Start();
            }
        }
    }
}
