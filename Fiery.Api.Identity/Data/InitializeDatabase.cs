using System;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fiery.Api.Identity.Data
{
    using Fiery.Api.Identity.Models;
    using IdentityServer4.MongoDb.Mappers;
    using IdentityServer4.MongoDb.Services;
    using IdentityServer4.MongoDb.Abstractions;
    using IdentityServer4.MongoDb.Configurations;

    public static class InitializeDatabase
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                // Seed application users
                using (var usersDbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    usersDbContext.Database.Migrate();

                    var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    var users = Configurations.Users.Get();
                    foreach (var user in users)
                    {
                        var appUser = userMgr.FindByNameAsync(user.UserName).Result;

                        if (appUser == null)
                        {
                            appUser = new ApplicationUser
                            {
                                UserName = user.UserName
                            };
                            var result = userMgr.CreateAsync(appUser, user.Password).Result;
                            if (!result.Succeeded)
                            {
                                throw new Exception(result.Errors.First().Description);
                            }
                            result = userMgr.AddClaimsAsync(appUser, user.Claims).Result;
                            if (!result.Succeeded)
                            {
                                throw new Exception(result.Errors.First().Description);
                            }
                            Console.WriteLine($"{appUser.UserName} created");
                        }
                        else
                        {
                            Console.WriteLine($"{appUser.UserName} already exists");
                        }
                    }
                }

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
