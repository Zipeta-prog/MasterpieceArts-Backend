using AuthService.Models.Dto;
using AuthService.Models;

namespace AuthService.Services.IService
{
    public interface IUser
    {
        Task<string> RegisterUser(RegisterUserDto userDto);

        Task<LoginResponseDto> loginUser(LoginRequestDto loginRequestDto);

        Task<bool> AssignUserRoles(string Email, string RoleName);

        Task<ApplicationUser> GetUserById(string Id);

        Task<List<ApplicationUser>> GetAllUsers();
    }
}
