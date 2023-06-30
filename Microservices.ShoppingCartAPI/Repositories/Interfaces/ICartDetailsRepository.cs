using Microservices.ShoppingCartAPI.Models;

namespace Microservices.ShoppingCartAPI.Repositories.Interfaces;

public interface ICartDetailsRepository
{
    Task<CartDetails> AddCartDetailsAsync(CartDetails cartDetails);
    Task<CartDetails> GetCartDetailsAsync(int productId, int cartHeaderId);
    Task<int> GetCartItemCountAsync(int cartHeaderId);
    Task<bool> RemoveCartDetailsAsync(int cartDetailsId);
}