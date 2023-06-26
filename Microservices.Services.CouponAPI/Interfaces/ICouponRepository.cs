using Microservices.Services.CouponAPI.Models;
using System.Threading.Tasks;

namespace Microservices.Services.CouponAPI.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByIdAsync(int id);
        Task<List<CouponDto>> GetCouponsAsync();
        Task<CouponDto> CreateCouponAsync(CouponDto couponDto);
        Task<CouponDto> UpdateCouponAsync(int id, CouponDto couponDto);
        Task<bool> DeleteCouponAsync(int id);
        Task<CouponDto> GetCouponByCodeAsync(string code);
    }
}
