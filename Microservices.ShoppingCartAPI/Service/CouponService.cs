using Microservices.Services.CouponAPI.Models;
using Microservices.Services.ProductAPI.Models.Dto;
using Microservices.ShoppingCartAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Microservices.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CouponDto> GetCoupon(string couponeCode)
        {
            HttpClient client = _httpClientFactory.CreateClient("Coupon");
            HttpResponseMessage response = await client.GetAsync($"/api/coupon/get-by-code/{couponeCode}");
            string apiContext = await response.Content.ReadAsStringAsync();
            try
            {
                ResponseDto<CouponDto>? deserializesContent = JsonConvert.DeserializeObject<ResponseDto<CouponDto>>(apiContext);
                return deserializesContent?.Result ?? new CouponDto();
            }
            catch (Exception ex)
            {
                return new CouponDto();
            }

        }
    }
}
