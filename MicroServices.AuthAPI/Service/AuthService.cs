using Microservices.Services.AuthAPI.Data;
using MicroServices.AuthAPI.Models;
using MicroServices.AuthAPI.Models.Dtos;
using MicroServices.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace MicroServices.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            AppDbContext db,
            IJwtTokenGenerator jwtTokenGenerator
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            ApplicationUser? user = _db.ApplicationUser.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if(user is not null)
            {
                if(!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                await _userManager.AddToRoleAsync(user, roleName);
                
                return true;
            }

            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            ApplicationUser? user = _db.ApplicationUser.FirstOrDefault(user => user.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if(user is null || !isValidPassword)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }


            IList<string> roles = await _userManager.GetRolesAsync(user);

            string jwtToken = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                ID = user.Id,
                Email = user.Email,
                Name = user.UserName,
                PhoneNumber = user.PhoneNumber
            };


            return new LoginResponseDto()
            {
                User = userDto,
                Token = jwtToken
            };

        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };

            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                
                if(result.Succeeded)
                {
                    ApplicationUser? userToReturn = _db.ApplicationUser.FirstOrDefault(user => user.UserName == registrationRequestDto.Email);
                   
                    UserDto userDto = new()
                    {
                        Email = registrationRequestDto.Email,
                        Name = registrationRequestDto.Name,
                        PhoneNumber = registrationRequestDto.PhoneNumber,
                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {
                return "Error encountered";
            }
        }
    }
}
