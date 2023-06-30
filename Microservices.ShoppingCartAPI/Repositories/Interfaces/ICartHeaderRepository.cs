using Microservices.ShoppingCartAPI.Models;

namespace Microservices.ShoppingCartAPI.Repositories.Interfaces;

public interface ICartHeaderRepository
{
    Task<CartHeader> GetCartHeaderAsync(int cartHeaderId);
    Task<bool> RemoveCartHeaderAsync(int cartHeaderId);
}