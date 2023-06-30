// CartHeaderRepository.cs

using Microservices.ShoppingCartAPI.Data;
using Microservices.ShoppingCartAPI.Models;
using Microservices.ShoppingCartAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservices.ShoppingCartAPI.Repositories
{
    public class CartHeaderRepository : ICartHeaderRepository
    {
        private readonly AppDbContext _db;

        public CartHeaderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<CartHeader> GetCartHeaderAsync(int cartHeaderId)
        {
            return await _db.CartHeader.FirstOrDefaultAsync(u => u.CartHeaderId == cartHeaderId);
        }

        public async Task<bool> RemoveCartHeaderAsync(int cartHeaderId)
        {
            CartHeader cartHeader = await _db.CartHeader.FirstOrDefaultAsync(u => u.CartHeaderId == cartHeaderId);
            if (cartHeader != null) 
            {
                _db.CartHeader.Remove(cartHeader);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
