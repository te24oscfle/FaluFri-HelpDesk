namespace FaluFri_HelpDesk.Models;

public enum TicketHistoryAction
{
    TicketCreated,
    FieldChanged
}

public class TicketHistory
{
    public int Id { get; set; }

    public int TicketId { get; set; }
    public int UserId { get; set; }

    public TicketHistoryAction Action { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }

    public DateTime CreatedAt { get; set; }

    public Ticket Ticket { get; set; } = null!;
    public User User { get; set; } = null!;
}