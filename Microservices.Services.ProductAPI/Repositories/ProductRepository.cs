using AutoMapper;
using Microservices.Services.CouponAPI.Data;
using Microservices.Services.ProductAPI.Models;
using Microservices.Services.ProductAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Services.ProductAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            return _mapper.Map<ProductDto>(await _context.Products.FirstOrDefaultAsync(product => product.ProductId == id));
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            return _mapper.Map<List<ProductDto>>(await _context.Products.ToListAsync());
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            Product? product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, ProductDto productDto)
        {
            Product? product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception($"Product with id {id} not found.");
            }
            _mapper.Map(productDto, product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            Product product = await _context.Products.FindAsync(id) ?? throw new Exception($"Product with id {id} not found.");
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
