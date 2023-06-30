using Microservices.ShoppingCartAPI.Models.Dto;

namespace Microservices.ShoppingCartAPI.Service.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
