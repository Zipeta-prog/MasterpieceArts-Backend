using EmailService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<EmailLogger> EmailLoggers { get; set; }
    }
}
