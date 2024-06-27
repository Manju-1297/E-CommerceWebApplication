using EmailNotificationModule.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailNotificationModule.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Ticket> Tickets { get; set; }
    }
}

