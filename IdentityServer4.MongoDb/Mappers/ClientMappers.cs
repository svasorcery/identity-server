using AutoMapper;
using System.Linq;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.MongoDb.Entities;

namespace IdentityServer4.MongoDb.Mappers
{
    public static class ClientMappers
    {
        internal static IMapper Mapper { get; }

        static ClientMappers()
        {
            Mapper = new MapperConfiguration(config => {
                ConfigureEntityToModelMapping(config);
                ConfigureModelToEntityMapping(config);
            }).CreateMapper();
        }


        public static Client ToModel(this StoredClient token)
        {
            return Mapper.Map<StoredClient, Client>(token);
        }

        public static StoredClient ToEntity(this Client token)
        {
            return Mapper.Map<Client, StoredClient>(token);
        }


        // private 

        private static IMapperConfigurationExpression ConfigureEntityToModelMapping(
           IMapperConfigurationExpression config
           )
        {
            config.CreateMap<StoredClient, Client>(MemberList.Destination)
                .ForMember(x => x.Claims,
                    opt => opt.MapFrom(src => src.Claims.Select(x => new Claim(x.Type, x.Value))))
                .ForMember(x => x.ClientSecrets,
                    opt => opt.MapFrom(src => src.ClientSecrets.Select(x => new Secret
                    { Type = x.Type, Value = x.Value, Description = x.Description, Expiration = x.Expiration, })));

            return config;
        }

        private static IMapperConfigurationExpression ConfigureModelToEntityMapping(
                IMapperConfigurationExpression config
                )
        {
            config.CreateMap<Client, StoredClient>(MemberList.Source)
                .ForMember(x => x.Claims,
                    opt => opt.MapFrom(src => src.Claims.Select(x => new StoredClaim
                    { Type = x.Type, Value = x.Value })))
                .ForMember(x => x.ClientSecrets,
                    opt => opt.MapFrom(src => src.ClientSecrets.Select(x => new StoredSecret
                    { Type = x.Type, Value = x.Value, Description = x.Description, Expiration = x.Expiration, })));

            return config;
        }
    }
}
