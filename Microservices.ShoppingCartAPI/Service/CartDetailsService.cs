using AutoMapper;
using Microservices.ShoppingCartAPI.Models.Dto;
using Microservices.ShoppingCartAPI.Models;
using Microservices.ShoppingCartAPI.Service.Interfaces;
using Microservices.ShoppingCartAPI.Data;
using Microservices.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microservices.ShoppingCartAPI.Service
{
    public class CartDetailsService : ICartDetailsService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CartDetailsService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CartDetails> AddCartDetails(CartDetails cartDetails)
        {
            _db.CartDetails.Add(cartDetails);
            await _db.SaveChangesAsync();

            return cartDetails;
        }

        public async Task<CartDetails> UpsertCartDetails(CartDto cartDto, CartHeader cartHeaderFromDb)
        {
            CartDetails? cartDetailsFromDb = await _db.CartDetails.AsNoTracking()
                .FirstOrDefaultAsync(u =>
                u.ProductId == cartDto.CartDetails.First().ProductId &&
                u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

            if (cartDetailsFromDb != null)
            {
                cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                await _db.SaveChangesAsync();
            }
            else
            {
                cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                await _db.SaveChangesAsync();
            }

            return cartDetailsFromDb;
        }

        public async Task<ResponseDto<bool>> RemoveCartDetails(int cartDetailsId)
        {
            ResponseDto<bool> responseDto = new();

            try
            {
                CartDetails cartDetails = await _db.CartDetails.FirstAsync(u => u.CartDetailsId == cartDetailsId);

                int totalCountOfCartItem = _db.CartDetails.Count(u => u.CartHeaderId == cartDetails.CartHeaderId);

                _db.CartDetails.Remove(cartDetails);

                if (totalCountOfCartItem == 1)
                {
                    CartHeader? cartHeaderToRemove = await _db.CartHeader.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    if (cartHeaderToRemove != null) _db.CartHeader.Remove(cartHeaderToRemove);
                }

                await _db.SaveChangesAsync();

                responseDto.Result = true;
            }
            catch (Exception ex)
            {
                responseDto.Result = false;
                responseDto.Message = ex.Message;
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }
    }
}