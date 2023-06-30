using Microservices.Services.ProductAPI.Models.Dto;
using Microservices.ShoppingCartAPI.Models.Dto;
using Microservices.ShoppingCartAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.ShoppingCartAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    [Authorize]
    public class CartAPIController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartAPIController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpPost("apply-coupon")]
        public async Task<ResponseDto<bool>> ApplyCoupon([FromBody] CartDto cartDto)
        {
            return await _cartService.ApplyCoupon(cartDto);
        }

        [HttpPost("email-cart-request")]
        public async Task<ResponseDto<bool>> EmailCartRequest([FromBody] CartDto cartDto)
        {
            return await _cartService.EmailCartRequest(cartDto);
        }

        [HttpPost("remove-coupon")]
        public async Task<ResponseDto<bool>> RemoveCoupon([FromBody] CartDto cartDto)
        {
            return await _cartService.RemoveCoupon(cartDto);
        }

        [HttpPost]
        public async Task<ResponseDto<CartDto>> CartUpsert([FromBody] CartDto cartDto)
        {
            return await _cartService.UpsertCart(cartDto);
        }

        [HttpGet("get-cart/{userId}")]
        public async Task<ResponseDto<CartDto>> GetCart(string userId)
        {
            return await _cartService.GetCart(userId);
        }

        [HttpDelete("remove-cart/{cartDetailsId:int}")]
        public async Task<ResponseDto<bool>> RemoveCartDetails(int cartDetailsId)
        {
            return await _cartService.RemoveCartDetails(cartDetailsId);
        }

    }
}
