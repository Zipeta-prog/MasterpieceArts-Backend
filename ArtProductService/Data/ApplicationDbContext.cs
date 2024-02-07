using ArtProductService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ArtProductService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Art> Arts { get; set; }
    }
}
