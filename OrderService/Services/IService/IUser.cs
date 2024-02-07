using OrderService.Models.Dto;

namespace OrderService.Services.IService
{
    public interface IUser
    {
        Task<UserDto> GetUserById(string Id);
    }
}
