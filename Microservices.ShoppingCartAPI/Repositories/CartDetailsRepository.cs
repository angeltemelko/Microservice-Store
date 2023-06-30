// CartDetailsRepository.cs

using Microservices.ShoppingCartAPI.Data;
using Microservices.ShoppingCartAPI.Models;
using Microservices.ShoppingCartAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservices.ShoppingCartAPI.Repositories
{
    public class CartDetailsRepository : ICartDetailsRepository
    {
        private readonly AppDbContext _db;

        public CartDetailsRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<CartDetails> AddCartDetailsAsync(CartDetails cartDetails)
        {
            _db.CartDetails.Add(cartDetails);
            await _db.SaveChangesAsync();

            return cartDetails;
        }

        public async Task<CartDetails> GetCartDetailsAsync(int productId, int cartHeaderId)
        {
            return await _db.CartDetails
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.ProductId == productId && u.CartHeaderId == cartHeaderId);
        }

        public async Task<int> GetCartItemCountAsync(int cartHeaderId)
        {
            return await _db.CartDetails.CountAsync(u => u.CartHeaderId == cartHeaderId);
        }

        public async Task<bool> RemoveCartDetailsAsync(int cartDetailsId)
        {
            CartDetails cartDetails = await _db.CartDetails.FirstAsync(u => u.CartDetailsId == cartDetailsId);

            _db.CartDetails.Remove(cartDetails);
            await _db.SaveChangesAsync();

            return true;
        }
    }
}