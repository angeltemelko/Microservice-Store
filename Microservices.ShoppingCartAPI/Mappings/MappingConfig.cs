using AutoMapper;
using Microservices.ShoppingCartAPI.Models;
using Microservices.ShoppingCartAPI.Models.Dto;

namespace Microservices.ShoppingCartAPI.Mappings
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            });
        }
    }
}