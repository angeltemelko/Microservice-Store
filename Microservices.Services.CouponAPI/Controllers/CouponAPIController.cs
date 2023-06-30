using Microservices.Services.CouponAPI.Interfaces;
using Microservices.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Microservices.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponAPIController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CouponDto>>>> GetAllCoupons()
        {
            ResponseDto<List<CouponDto>> responseDto = new();

            try 
            {
                responseDto.Result = await _couponRepository.GetCouponsAsync();
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponseDto<CouponDto>>> GetCouponById([FromQuery] int id)
        {
            ResponseDto<CouponDto> responseDto = new();

            try
            {
                responseDto.Result = await _couponRepository.GetCouponByIdAsync(id);
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.IsSuccess = false;
            }

            return responseDto;

        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDto<CouponDto>>> CreateCoupon([FromBody] CouponDto couponDto)
        {
            ResponseDto<CouponDto> responseDto = new();

            try
            {
                responseDto.Result = await _couponRepository.CreateCouponAsync(couponDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDto<CouponDto>>> UpdateCoupon(int id, [FromBody] CouponDto couponDto)
        {
            ResponseDto<CouponDto> responseDto = new();

            try
            {
                responseDto.Result = await _couponRepository.UpdateCouponAsync(id, couponDto);
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<ResponseDto<bool>>> DeleteCoupon(int id)
        {
            ResponseDto<bool> responseDto = new();

            try
            {
                responseDto.Result = await _couponRepository.DeleteCouponAsync(id);
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }

        [HttpGet("get-by-code/{code}")]
        public async Task<ActionResult<ResponseDto<CouponDto>>> GetCouponByCode(string code)
        {
            ResponseDto<CouponDto> responseDto = new();

            try
            {
                responseDto.Result = await _couponRepository.GetCouponByCodeAsync(code);
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.IsSuccess = false;
            }

            return responseDto;
        }

    }
}
