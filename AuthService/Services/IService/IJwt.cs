using AuthService.Models;

namespace AuthService.Services.IService
{
    public interface IJwt
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> Roles);
    }
}
