using Microservices.Services.ProductAPI.Models;
using Microservices.Services.ProductAPI.Models.Dto;
using Microservices.Services.ProductAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private static async Task<ActionResult<ResponseDto<T>>> Execute<T>(Func<Task<T>> action)
        {
            ResponseDto<T> responseDto = new();
            try
            {
                responseDto.Result = await action();
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }

        [HttpGet]
        public Task<ActionResult<ResponseDto<List<ProductDto>>>> GetAllProducts()
            => Execute(async () => await _productRepository.GetProductsAsync());

        [HttpGet("{id:int}")]
        public Task<ActionResult<ResponseDto<ProductDto>>> GetProductById(int id)
            => Execute(async () => await _productRepository.GetProductByIdAsync(id));

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public Task<ActionResult<ResponseDto<ProductDto>>> CreateProduct([FromBody] ProductDto productDto)
            => Execute(async () => await _productRepository.CreateProductAsync(productDto));

        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public Task<ActionResult<ResponseDto<ProductDto>>> UpdateProduct(int id, [FromBody] ProductDto productDto)
            => Execute(async () => await _productRepository.UpdateProductAsync(id, productDto));

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public Task<ActionResult<ResponseDto<bool>>> DeleteProduct(int id)
            => Execute(async () => await _productRepository.DeleteProductAsync(id));
    }
}
