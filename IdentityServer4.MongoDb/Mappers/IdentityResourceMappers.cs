using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.MongoDb.Entities;

namespace IdentityServer4.MongoDb.Mappers
{
    public static class IdentityResourceMappers
    {
        internal static IMapper Mapper { get; }

        static IdentityResourceMappers()
        {
            Mapper = new MapperConfiguration(config => {
                ConfigureEntityToModelMapping(config);
                ConfigureModelToEntityMapping(config);
            }).CreateMapper();
        }

        public static IdentityResource ToModel(this StoredIdentityResource token)
        {
            return Mapper.Map<StoredIdentityResource, IdentityResource>(token);
        }

        public static StoredIdentityResource ToEntity(this IdentityResource token)
        {
            return Mapper.Map<IdentityResource, StoredIdentityResource>(token);
        }


        // private 

        private static IMapperConfigurationExpression ConfigureEntityToModelMapping(
           IMapperConfigurationExpression config
           )
        {
            config.CreateMap<StoredIdentityResource, IdentityResource>(MemberList.Destination);

            return config;
        }

        private static IMapperConfigurationExpression ConfigureModelToEntityMapping(
                IMapperConfigurationExpression config
                )
        {
            config.CreateMap<IdentityResource, StoredIdentityResource>(MemberList.Source);

            return config;
        }
    }
}
