using BiddingService.Models.Dto;

namespace BiddingService.Services.IService
{
    public interface IArt
    {
        Task<ArtDto> GetArtById(string Id);

    }
}
