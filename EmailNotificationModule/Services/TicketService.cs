using EmailNotificationModule.Data;
using EmailNotificationModule.Models;
using EmailNotificationModule.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmailNotificationModule.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext dbContext;

        public TicketService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<object> RaiseTicketAsync(Ticket tickets)
        {
            var ticket = new Ticket
            {
                UserId = tickets.UserId,
                Priority = tickets.Priority,
                Module = tickets.Module,
                Title = tickets.Title,
                OrderId = tickets.OrderId,
                Description = tickets.Description
            };

            dbContext.Tickets.Add(ticket);
            await dbContext.SaveChangesAsync();

            string ticketReference = GenerateTicketReference(tickets.OrderId);

            return ticketReference;
        }

        private string GenerateTicketReference(string orderId)
        {
            // Example logic to generate a fixed ticket reference
            return "8989756454";
        }
    }
}


