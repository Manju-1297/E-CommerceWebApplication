//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using MimeKit;
//using TicketProcessingApp.Data;
//using TicketProcessingApp.Models;

//namespace TicketProcessingApp.Services
//{
//    public class TicketProcessorService:IHostedService,IDisposable
//    {
//        private Timer _timer;
//        private readonly AppDbContext dbContext;
//        private readonly ILogger<TicketProcessorService> logger;

//        public TicketProcessorService(AppDbContext dbContext,ILogger<TicketProcessorService> logger) 
//        {
//            this.dbContext = dbContext;
//            this.logger = logger;
//        }

//        public Task StartAsync(CancellationToken cancellationToken) 
//        {
//            logger.LogInformation("Ticket Processor Service is starting");
//            _timer = new Timer(ProcessTickets, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
//        return Task.CompletedTask;
//        }

//        private async void ProcessTickets(object? state)
//        {
//            logger.LogInformation("Processing Tickets...");
//            var highPriorityTickets = await dbContext.TicketSet
//                .Where(t => t.Priority == "High" && !t.EmailSent)
//                .ToListAsync();

//            foreach (var ticket in highPriorityTickets)
//            {
//                sendEmailAlert(ticket);
//                ticket.EmailSent = true;
//            }
//             dbContext.SaveChanges();
//        }

//        private void sendEmailAlert(Ticket ticket)
//        {
//            var message = new MimeMessage();
//            message.From.Add(new MailboxAddress("Manjunath", "Manju4mdharwad@gmail.com"));
//            message.To.Add(new MailboxAddress("Admin", "Admin@gmail.com"));
//            message.Subject = "High Priority Ticket Notification";

//            var bodyBuilder = new BodyBuilder()
//            {
//                TextBody=$"Ticket Details:\nUser ID: {ticket.UserId}\nPriority: {ticket.Priority}\n" +
//                           $"Module: {ticket.Module}\nTitle: {ticket.Title}\nOrder ID: {ticket.OrderId}\n" +
//                           $"Description: {ticket.Description}"
//            };

//            message.Body=bodyBuilder.ToMessageBody();
//            using (var client = new SmtpClient())
//            {
//                client.Connect("smtp.example.com", 587, false); // Use your SMTP server details
//                client.Authenticate("your-email@example.com", "your-password");

//                client.Send(message);
//                client.Disconnect(true);
//            }
//        }

//        public Task StopAsync(CancellationToken cancellationToken) 
//        {
//            logger.LogInformation("Ticket Processor Service is stopping.");
//            _timer?.Change(Timeout.Infinite, 0);
//            return Task.CompletedTask;

//        }
//        public void Dispose()
//        {
//            _timer?.Dispose();
//        }
//    }
//}

using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TicketProcessingApp.Models;
using TicketProcessingApp.Data;

namespace TicketProcessor
{
    public class TicketProcessorService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly AppDbContext _dbContext;
        private readonly ILogger<TicketProcessorService> _logger;

        public TicketProcessorService(AppDbContext dbContext, ILogger<TicketProcessorService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Ticket Processor Service is starting.");
            _timer = new Timer(ProcessTickets, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        private async void ProcessTickets(object state)
        {
            _logger.LogInformation("Processing tickets...");

            var highPriorityTickets = await _dbContext.TicketSet
                .Where(t => t.Priority == "High" && !t.EmailSent)
                .ToListAsync();

            foreach (var ticket in highPriorityTickets)
            {
                SendEmailAlert(ticket);
                ticket.EmailSent = true;
            }

            await _dbContext.SaveChangesAsync();
        }

        private void SendEmailAlert(Ticket ticket)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Manjunath K", "mk12dwd@.com"));
            message.To.Add(new MailboxAddress("Manjunath Kurubagatti", "manju4mdharwad@gmail.com.com"));
            message.Subject = "High Priority Ticket Notification";

            var bodyBuilder = new BodyBuilder
            {
                TextBody = $"Ticket details:\nUser ID: {ticket.UserId}\nPriority: {ticket.Priority}\n" +
                           $"Module: {ticket.Module}\nTitle: {ticket.Title}\nOrder ID: {ticket.OrderId}\n" +
                           $"Description: {ticket.Description}"
            };

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.example.com", 587, false); // Use your SMTP server details
                client.Authenticate("mk12dwd@gmail.com", "Manju@2022");

                client.Send(message);
                client.Disconnect(true);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Ticket Processor Service is stopping.");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
