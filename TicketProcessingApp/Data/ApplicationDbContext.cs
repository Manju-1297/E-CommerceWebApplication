using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketProcessingApp.Models;

namespace TicketProcessingApp.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Ticket> TicketSet { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
                
        }
    }
}
