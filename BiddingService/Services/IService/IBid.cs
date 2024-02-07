using BiddingService.Models;

namespace BiddingService.Services.IService
{
    public interface IBid
    {
        Task<string> AddBid(Bids bid);
        Task<List<Bids>> GetArBids(Guid ArtId);
        Task<List<Bids>> HighestBidsPerItem(Guid userId);
        Task<List<Bids>> GetMyBids(Guid userId);
        Task<Bids> GetOneBid(Guid Id);
        Task<List<Bids>> GetMyWins(Guid userId);

        //Task<string> UpdateBid();
        Task<string> DeleteBid(Bids art);
    }
}
