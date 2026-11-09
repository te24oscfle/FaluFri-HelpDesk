namespace FaluFri_HelpDesk.Models;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public ICollection<Ticket> CreatedTickets = new List<Ticket>();
    public ICollection<Ticket> AssignedTickets = new List<Ticket>();
}