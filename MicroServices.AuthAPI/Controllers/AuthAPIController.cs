using Microservices.Services.AuthAPI.Models;
using MicroServices.AuthAPI.Models.Dtos;
using MicroServices.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroServices.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            ResponseDto<string> responseDto = new();

            string errorMessage = await _authService.Register(model);
            if (errorMessage != null)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = errorMessage;
                return BadRequest(errorMessage);
            }

            return Ok(responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            ResponseDto<LoginResponseDto> responseDto = new();

            LoginResponseDto loginResponseDto = await _authService.Login(model);

            if (loginResponseDto == null)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Username or password is incorrect";
                return BadRequest(loginResponseDto);
            }

            responseDto.Result = loginResponseDto;

            return Ok(responseDto);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            ResponseDto<bool> responseDto = new();

            bool assignRoleResponse = await _authService.AssignRole(model.Email, model.Role);

            if (!assignRoleResponse)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Username or password is incorrect";
                return BadRequest(assignRoleResponse);
            }

            responseDto.Result = assignRoleResponse;

            return Ok(responseDto);
        }
    }
}
