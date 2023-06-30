using AutoMapper;
using Microservices.ShoppingCartAPI.Models.Dto;
using Microservices.ShoppingCartAPI.Models;
using Microservices.ShoppingCartAPI.Service.Interfaces;
using Microservices.ShoppingCartAPI.Data;
using Microservices.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microservices.Services.CouponAPI.Models;
using Microservices.MessageBus;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.ShoppingCartAPI.Service
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ICartDetailsService _cartDetailsService;
        private readonly ICouponService _couponService;
        private readonly IProductService _productService;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _config;

        public CartService(AppDbContext db, IMapper mapper, ICartDetailsService cartDetailsService,
            IProductService productService, ICouponService couponService, IMessageBus messageBus, IConfiguration config)
        {
            _db = db;
            _mapper = mapper;
            _cartDetailsService = cartDetailsService;
            _productService = productService;
            _couponService = couponService;
            _messageBus = messageBus;
            _config = config;
        }

        public async Task<ResponseDto<CartDto>> UpsertCart(CartDto cartDto)
        {
            ResponseDto<CartDto> responseDto = new();

            try
            {
                CartHeader? cartHeaderFromDb = await _db.CartHeader.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserId == cartDto.CartHeader.UserId);

                if (cartHeaderFromDb == null)
                {
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _db.CartHeader.Add(cartHeader);
                    await _db.SaveChangesAsync();

                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;

                    await _cartDetailsService.AddCartDetails(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                }
                else
                {
                    await _cartDetailsService.UpsertCartDetails(cartDto, cartHeaderFromDb);
                }

                responseDto.Result = cartDto;
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message.ToString();
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }

        public async Task<ResponseDto<CartDto>> GetCart(string userId)
        {
            ResponseDto<CartDto> responseDto = new();

            try
            {
                CartDto cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(await _db.CartHeader.FirstAsync(u => u.UserId == userId)),
                };

                cart.CartDetails =
                    _mapper.Map<IEnumerable<CartDetailsDto>>(_db.CartDetails.Where(u =>
                        u.CartHeaderId == cart.CartHeader.CartHeaderId));

                IEnumerable<ProductDto> productDtos = await _productService.GetProducts();

                foreach (var item in cart.CartDetails)
                {
                    item.Product = productDtos.FirstOrDefault(u => u.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }

                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDto coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cart.CartHeader.Discount = coupon.DiscountAmount;
                    }
                }

                responseDto.Result = cart;
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message.ToString();
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }

        public async Task<ResponseDto<bool>> RemoveCartDetails(int cartDetailsId)
        {
            return await _cartDetailsService.RemoveCartDetails(cartDetailsId);
        }

        public async Task<ResponseDto<bool>> ApplyCoupon(CartDto cartDto)
        {
            ResponseDto<bool> responseDto = new();

            try
            {
                var cartFromDb = await _db.CartHeader.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                _db.CartHeader.Update(cartFromDb);
                await _db.SaveChangesAsync();
                responseDto.Result = true;
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message.ToString();
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }

        public async Task<ResponseDto<bool>> RemoveCoupon(CartDto cartDto)
        {
            ResponseDto<bool> responseDto = new();

            try
            {
                var cartFromDb = await _db.CartHeader.FirstAsync(u => u.UserId == cartDto.CartHeader.UserId);
                cartFromDb.CouponCode = "";
                _db.CartHeader.Update(cartFromDb);
                await _db.SaveChangesAsync();
                responseDto.Result = true;
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message.ToString();
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }

        public async Task<ResponseDto<bool>> EmailCartRequest([FromBody] CartDto cartDto)
        {
            ResponseDto<bool> responseDto = new();
            try
            {
                await _messageBus.PublishMessage(cartDto,
                    _config.GetValue<string>("TopicAndQueueNames:EmailShoppingCart"),
                    _config.GetValue<string>("ServiceBus:ConnectionString"));
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message.ToString();
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }
    }
}