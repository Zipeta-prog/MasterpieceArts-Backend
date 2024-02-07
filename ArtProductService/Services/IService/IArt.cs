using ArtProductService.Models;

namespace ArtProductService.Services.IService
{
    public interface IArt
    {
        Task<List<Art>> GetAllArts();
        Task<Art> GetOneArt(Guid Id);
        Task<List<Art>> GetMyArts(Guid userId);
        Task<string> AddArt(Art art);
        Task<string> UpdateArt();
        Task<string> DeleteArt(Art art);

    }
}
