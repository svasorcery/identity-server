using AutoMapper;
using System.Linq;
using IdentityServer4.Models;
using IdentityServer4.MongoDb.Entities;

namespace IdentityServer4.MongoDb.Mappers
{
    public static class ScopeMappers
    {
        internal static IMapper Mapper { get; }

        static ScopeMappers()
        {
            Mapper = new MapperConfiguration(config => {
                ConfigureEntityToModelMapping(config);
                ConfigureModelToEntityMapping(config);
            }).CreateMapper();
        }

        public static Scope ToModel(this StoredScope token)
        {
            return Mapper.Map<StoredScope, Scope>(token);
        }

        public static StoredScope ToEntity(this Scope token)
        {
            return Mapper.Map<Scope, StoredScope>(token);
        }


        // private 

        private static IMapperConfigurationExpression ConfigureEntityToModelMapping(
           IMapperConfigurationExpression config
           )
        {
            config.CreateMap<StoredScope, Scope>(MemberList.Destination);

            return config;
        }

        private static IMapperConfigurationExpression ConfigureModelToEntityMapping(
                IMapperConfigurationExpression config
                )
        {
            config.CreateMap<Scope, StoredScope>(MemberList.Source);

            return config;
        }
    }
}
