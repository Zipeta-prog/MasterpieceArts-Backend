using ArtProductService.Models.Dto;

namespace ArtProductService.Services.IService
{
    public interface IUser
    {
        Task<UserDto> GetUserById(string Id);
    }
}
