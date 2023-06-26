using AutoMapper;
using Microservices.Services.CouponAPI.Data;
using Microservices.Services.CouponAPI.Interfaces;
using Microservices.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace Microservices.Services.CouponAPI.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;

        public CouponRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponDto> GetCouponByIdAsync(int id)
        {
            return _mapper.Map<CouponDto>(await _context.Coupons.FirstOrDefaultAsync(coupon => coupon.CouponId == id));
        }

        public async Task<List<CouponDto>> GetCouponsAsync()
        {
            return _mapper.Map<List<CouponDto>>(await _context.Coupons.ToListAsync());
        }

        public async Task<CouponDto> CreateCouponAsync(CouponDto couponDto)
        {
            var coupon = _mapper.Map<Coupon>(couponDto);
            _context.Coupons.Add(coupon);
            await _context.SaveChangesAsync();
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<CouponDto> UpdateCouponAsync(int id, CouponDto couponDto)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
            {
                throw new Exception($"Coupon with id {id} not found.");
            }
            _mapper.Map(couponDto, coupon);
            await _context.SaveChangesAsync();
            return _mapper.Map<CouponDto>(coupon);
        }

        public async Task<bool> DeleteCouponAsync(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id) ?? throw new Exception($"Coupon with id {id} not found.");
            _context.Coupons.Remove(coupon);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CouponDto> GetCouponByCodeAsync(string code)
        {
            return _mapper.Map<CouponDto>(await _context.Coupons.FirstOrDefaultAsync(coupon => coupon.CouponCode == code));
        }
    }
}
