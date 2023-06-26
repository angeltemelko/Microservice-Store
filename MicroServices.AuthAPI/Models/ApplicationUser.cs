using Microsoft.AspNetCore.Identity;

namespace MicroServices.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
