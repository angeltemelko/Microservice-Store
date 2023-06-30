using Microservices.Services.CouponAPI.Models;

namespace Microservices.ShoppingCartAPI.Service.Interfaces
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
