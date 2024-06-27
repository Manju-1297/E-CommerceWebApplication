using EmailNotificationModule.Models;

namespace EmailNotificationModule.Services.Interfaces
{
    public interface ITicketService
    {
        Task<Object> RaiseTicketAsync(Ticket ticket);
    }
}
