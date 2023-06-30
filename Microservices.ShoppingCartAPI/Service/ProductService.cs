using Microservices.Services.ProductAPI.Models.Dto;
using Microservices.ShoppingCartAPI.Models.Dto;
using Microservices.ShoppingCartAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Microservices.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ProductApi = "/api/products";

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            HttpClient client = _httpClientFactory.CreateClient("Product");
            HttpResponseMessage response = await client.GetAsync(ProductApi);
            string apiContext = await response.Content.ReadAsStringAsync();
            try
            {
                ResponseDto<IEnumerable<ProductDto>>? responseDto =
                    JsonConvert.DeserializeObject<ResponseDto<IEnumerable<ProductDto>>>(apiContext);
                return responseDto?.Result ?? Array.Empty<ProductDto>();
            }
            catch (Exception)
            {
                return new List<ProductDto>();
            }
        }
    }
}