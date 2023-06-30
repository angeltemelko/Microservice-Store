using Microservices.Services.ProductAPI.Models.Dto;
using Microservices.ShoppingCartAPI.Models.Dto;

namespace Microservices.ShoppingCartAPI.Service.Interfaces
{
    public interface ICartService
    {
        Task<ResponseDto<CartDto>> UpsertCart(CartDto cartDto);
        Task<ResponseDto<CartDto>> GetCart(string userId);
        Task<ResponseDto<bool>> RemoveCartDetails(int cartDetailsId);
        Task<ResponseDto<bool>> ApplyCoupon(CartDto cartDto);
        Task<ResponseDto<bool>> RemoveCoupon(CartDto cartDto);
        Task<ResponseDto<bool>> EmailCartRequest(CartDto cartDto);

    }

}
