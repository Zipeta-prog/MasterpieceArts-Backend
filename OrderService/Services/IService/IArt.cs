using OrderService.Models.Dto;

namespace OrderService.Services.IService
{
    public interface IArt
    {
        Task<ArtDto> GetArtById(string Id);
    }
}
