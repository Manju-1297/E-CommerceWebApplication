using EmailNotificationModule.Models;
using EmailNotificationModule.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailNotificationModule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("RaiseTicket")]
        public async Task<IActionResult> RaiseTicket([FromBody] Ticket ticket)
        {
            try
            {
                var referenceNumber = await _ticketService.RaiseTicketAsync(ticket);
                return Ok(new { success = true, message = $"Ticket raised successfully. Ticket Reference #{referenceNumber}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Failed to raise ticket: {ex.Message}" });
            }
        }
    }
}



