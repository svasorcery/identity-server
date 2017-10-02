using AutoMapper;
using System.Linq;
using IdentityServer4.Models;
using IdentityServer4.MongoDb.Entities;

namespace IdentityServer4.MongoDb.Mappers
{
    public static class ApiResourceMappers
    {
        internal static IMapper Mapper { get; }

        static ApiResourceMappers()
        {
            Mapper = new MapperConfiguration(config => {
                ConfigureEntityToModelMapping(config);
                ConfigureModelToEntityMapping(config);
            }).CreateMapper();
        }


        public static ApiResource ToModel(this StoredApiResource token)
        {
            return Mapper.Map<StoredApiResource, ApiResource>(token);
        }

        public static StoredApiResource ToEntity(this ApiResource token)
        {
            return Mapper.Map<ApiResource, StoredApiResource>(token);
        }


        // private 

        private static IMapperConfigurationExpression ConfigureEntityToModelMapping(
           IMapperConfigurationExpression config
           )
        {
            config.CreateMap<StoredApiResource, ApiResource>(MemberList.Destination)
                .ForMember(x => x.ApiSecrets,
                    opt => opt.MapFrom(src => src.ApiSecrets.Select(x => new Secret
                    { Type = x.Type, Value = x.Value, Description = x.Description, Expiration = x.Expiration, })))
                .ForMember(x => x.Scopes,
                    opt => opt.MapFrom(src => src.Scopes.Select(x => new Scope
                    {
                        Description = x.Description,
                        DisplayName = x.DisplayName,
                        Emphasize = x.Emphasize,
                        Name = x.Name,
                        Required = x.Required,
                        ShowInDiscoveryDocument = x.ShowInDiscoveryDocument,
                        UserClaims = x.UserClaims,
                    })));

            return config;
        }

        private static IMapperConfigurationExpression ConfigureModelToEntityMapping(
                IMapperConfigurationExpression config
                )
        {
            config.CreateMap<ApiResource, StoredApiResource>(MemberList.Source)
                .ForMember(x => x.ApiSecrets, opt => opt.MapFrom(src => src.ApiSecrets.Select(x => new StoredSecret { Type = x.Type, Value = x.Value, Description = x.Description, Expiration = x.Expiration, })))
                .ForMember(x => x.Scopes, opt => opt.MapFrom(src => src.Scopes.Select(x => new StoredScope
                {
                    Description = x.Description,
                    DisplayName = x.DisplayName,
                    Emphasize = x.Emphasize,
                    Name = x.Name,
                    Required = x.Required,
                    ShowInDiscoveryDocument = x.ShowInDiscoveryDocument,
                    UserClaims = x.UserClaims,
                })));

            return config;
        }
    }
}
