using Microservices.Services.ProductAPI.Models;

namespace Microservices.Services.ProductAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<ProductDto>> GetProductsAsync();
        Task<ProductDto> CreateProductAsync(ProductDto productDto);
        Task<ProductDto> UpdateProductAsync(int id, ProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
