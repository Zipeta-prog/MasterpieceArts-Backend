using BiddingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BiddingService.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Bids> Bid { get; set; }
    }
}
