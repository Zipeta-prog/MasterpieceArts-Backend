using Microsoft.AspNetCore.Identity;

namespace AuthService.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
