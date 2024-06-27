namespace EmailNotificationModule.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Priority { get; set; }
        public string? Module { get; set; }
        public string? Title { get; set; }
        public string? OrderId { get; set; }
        public string? Description { get; set; }
    }

}
