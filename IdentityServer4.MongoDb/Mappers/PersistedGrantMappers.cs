using AutoMapper;
using IdentityServer4.Models;
using IdentityServer4.MongoDb.Entities;

namespace IdentityServer4.MongoDb.Mappers
{
    public static class PersistedGrantMappers
    {
        internal static IMapper Mapper { get; }

        static PersistedGrantMappers()
        {
            Mapper = new MapperConfiguration(config => {
                ConfigureEntityToModelMapping(config);
                ConfigureModelToEntityMapping(config);
            }).CreateMapper();
        }

        public static PersistedGrant ToModel(this StoredPersistedGrant token)
        {
            return Mapper.Map<StoredPersistedGrant, PersistedGrant>(token);
        }

        public static StoredPersistedGrant ToEntity(this PersistedGrant token)
        {
            return Mapper.Map<PersistedGrant, StoredPersistedGrant>(token);
        }


        // private 

        private static IMapperConfigurationExpression ConfigureEntityToModelMapping(
           IMapperConfigurationExpression config
           )
        {
            config.CreateMap<StoredPersistedGrant, PersistedGrant>(MemberList.Destination);

            return config;
        }

        private static IMapperConfigurationExpression ConfigureModelToEntityMapping(
                IMapperConfigurationExpression config
                )
        {
            config.CreateMap<PersistedGrant, StoredPersistedGrant>(MemberList.Source);

            return config;
        }
    }
}
