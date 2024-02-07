using BiddingService.Data;
using BiddingService.Models;
using BiddingService.Services.IService;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BiddingService.Services
{
    public class BidService : IBid
    {
        private readonly ApplicationDbContext _context;
        public BidService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddBid(Bids bid)
        {
            _context.Bid.Add(bid);
            await _context.SaveChangesAsync();
            return "Bid was successful";
        }

        public async Task<string> DeleteBid(Bids art)
        {
            _context.Bid.Remove(art);
            await _context.SaveChangesAsync();
            return "Bid removed successfully";
        }

        public async Task<List<Bids>> GetArBids(Guid ArtId)
        {
            return await _context.Bid.Where(b => b.ArtId == ArtId).ToListAsync();
        }

        public async Task<List<Bids>> GetMyBids(Guid userId)
        {
            return await _context.Bid.Where(b => b.BidderId == userId).ToListAsync();
        }

        public async Task<List<Bids>> GetMyWins(Guid userId)
        {
            var MyWins = await _context.Bid
            .Where(b => b.BidderId == userId)
            .GroupBy(b => b.ArtId)
            .Select(group =>
                group.OrderByDescending(b => b.BidAmmount)
                     .FirstOrDefault(b => b.BidderId == group.OrderByDescending(x => x.BidAmmount).First().BidderId))
            .ToListAsync();

            return MyWins;

        }

        public async Task<Bids> GetOneBid(Guid Id)
        {
            return await _context.Bid.Where(b => b.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Bids>> HighestBidsPerItem(Guid userId)
        {
            var highestBids = await _context.Bid
                .Where(b => b.BidderId == userId)
                .GroupBy(b => b.ArtId)
                .Select(g => g.OrderByDescending(b => b.BidAmmount).FirstOrDefault()) // Select the highest bid for each ArtId
                .ToListAsync();

            return highestBids;
        }
    }
}
