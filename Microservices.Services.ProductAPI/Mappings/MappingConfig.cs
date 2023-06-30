using AutoMapper;
using Microservices.Services.ProductAPI.Models;

namespace Microservices.Services.CouponAPI.Mappings
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
            });
        }
    }
}
