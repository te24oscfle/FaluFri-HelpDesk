namespace FaluFri_HelpDesk.Models;

public enum TicketStatus
{
    Open,
    InProgress,
    OnHold,
    Resolved
}

public enum TicketPriority
{
    Low,
    Medium,
    High,
    Critical
}

public class Ticket
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public TicketStatus Status { get; set; } = TicketStatus.Open;
    public TicketPriority Priority { get; set; } = TicketPriority.Low;

    public DateTime? ResolvedAt { get; set; }
    public string? ResolutionDescription { get; set; }

    public int CreatedByInt { get; set; }
    public User CreatedBy { get; set; } = null!;

    public int? AssignedToInt { get; set; }
    public User? AssignedTo { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int Version { get; set; } = 1;

    public ICollection<TicketHistory> History = new List<TicketHistory>();
}