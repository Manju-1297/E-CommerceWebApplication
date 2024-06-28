using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using TicketProcessingApp.Data;
using TicketProcessingApp.Models;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            ProcessTickets();
            Thread.Sleep(60000); // Wait for 60 seconds
        }
    }

    public static void ProcessTickets()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseInMemoryDatabase("TicketList");

        using (var context = new AppDbContext(optionsBuilder.Options))
        {
            var highPriorityTickets = context.TicketSet
                .Where(t => t.Priority == "High" && !t.EmailSent)
                .ToList();

            foreach (var ticket in highPriorityTickets)
            {
                SendEmail(ticket);
                ticket.EmailSent = true;
            }

            context.SaveChanges();
        }
    }

    public static void SendEmail(TicketModel ticket)
    {
        try
        {
            var mail = new MailMessage("mk12dwd@gmail.com", "manju4m@gmail.com");
            mail.Subject = $"High Priority Ticket: {ticket.Title}";
            mail.Body = $"Ticket Details:\n\n" +
                        $"User: {ticket.UserId}\n" +
                        $"Module: {ticket.Module}\n" +
                        $"Order ID: {ticket.OrderId}\n" +
                        $"Description: {ticket.Description}";

            using (var smtpClient = new SmtpClient("smtp.example.com"))
            {
                smtpClient.Credentials = new System.Net.NetworkCredential("mk12dwd@gmail.com", "");
                smtpClient.Send(mail);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send email: {ex.Message}");
        }
    }
}