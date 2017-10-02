using System;
using Microsoft.Extensions.Configuration;
using IdentityServer4.Stores;
using IdentityServer4.Services;
using IdentityServer4.MongoDb.Stores;
using IdentityServer4.MongoDb.Services;
using IdentityServer4.MongoDb.DbContexts;
using IdentityServer4.MongoDb.Abstractions;
using IdentityServer4.MongoDb.Configurations;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerMongoDBBuilderExtensions
    {
        #region ConfigurationStore

        public static IIdentityServerBuilder AddConfigurationStore(
           this IIdentityServerBuilder builder, Action<MongoDbRepositoryConfiguration> setupAction)
        {
            // Add Store Configuration
            builder.Services.Configure(setupAction);

            return builder.AddConfigurationStore();
        }

        public static IIdentityServerBuilder AddConfigurationStore(
            this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            // Add Store Configuration
            builder.Services.Configure<MongoDbRepositoryConfiguration>(configuration);

            return builder.AddConfigurationStore();
        }

        private static IIdentityServerBuilder AddConfigurationStore(
            this IIdentityServerBuilder builder)
        {
            // Add Database Contexts
            builder.Services.AddScoped<IClientDbContext, ClientDbContext>();
            builder.Services.AddScoped<IResourceDbContext, ResourceDbContext>();
            builder.Services.AddScoped<IScopeDbContext, ScopeDbContext>();

            // Add Configuration Stores
            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddTransient<IResourceStore, ResourceStore>();

            return builder;
        }

        #endregion ConfigurationStore


        #region OperationalStore

        public static IIdentityServerBuilder AddOperationalStore(
           this IIdentityServerBuilder builder, Action<MongoDbRepositoryConfiguration> setupAction)
        {
            builder.Services.Configure(setupAction);

            return builder.AddOperationalStore();
        }

        public static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<MongoDbRepositoryConfiguration>(configuration);

            return builder.AddOperationalStore();
        }

        private static IIdentityServerBuilder AddOperationalStore(
            this IIdentityServerBuilder builder)
        {
            // Add Database Contexts
            builder.Services.AddScoped<IPersistedGrantDbContext, PersistedGrantDbContext>();

            // Add Operational Stores
            builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();

            return builder;
        }

        #endregion OperationalStore
    }
}
