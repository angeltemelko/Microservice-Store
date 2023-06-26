using MicroServices.AuthAPI.Models.Dtos;

namespace MicroServices.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        public Task<string> Register(RegistrationRequestDto registrationRequestDto);

        public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        public Task<bool> AssignRole(string email, string roleName);

    }
}
