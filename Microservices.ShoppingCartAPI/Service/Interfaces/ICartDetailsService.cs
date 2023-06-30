using Microservices.Services.ProductAPI.Models.Dto;
using Microservices.ShoppingCartAPI.Models;
using Microservices.ShoppingCartAPI.Models.Dto;

namespace Microservices.ShoppingCartAPI.Service.Interfaces
{
    public interface ICartDetailsService
    {
        Task<CartDetails> AddCartDetails(CartDetails cartDetails);
        Task<CartDetails> UpsertCartDetails(CartDto cartDto, CartHeader cartHeaderFromDb);
        Task<ResponseDto<bool>> RemoveCartDetails(int cartDetailsId);
    }
}
