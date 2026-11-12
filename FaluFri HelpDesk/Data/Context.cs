using Microsoft.EntityFrameworkCore;
using FaluFri_HelpDesk.Models;

namespace FaluFri_HelpDesk.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketHistory> TicketHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ================================
        // ==== USER
        // ================================
        modelBuilder.Entity<User>()
            .Property(user => user.Username)
            .HasMaxLength(50);

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(user => user.PasswordHash)
            .HasMaxLength(255);

        modelBuilder.Entity<User>()
            .Property(user => user.IsActive)
            .HasDefaultValue(true);

        // ================================
        // ==== TICKET
        // ================================
        modelBuilder.Entity<Ticket>()
            .Property(ticket => ticket.Title)
            .HasMaxLength(100);

        modelBuilder.Entity<Ticket>()
            .Property(ticket => ticket.Description)
            .HasColumnType("TEXT");

        modelBuilder.Entity<Ticket>()
            .Property(ticket => ticket.Status)
            .HasConversion<string>()
            .HasMaxLength(20)
            .HasDefaultValue(TicketStatus.Open);

        modelBuilder.Entity<Ticket>()
            .Property(ticket => ticket.Priority)
            .HasConversion<string>()
            .HasMaxLength(20)
            .HasDefaultValue(TicketPriority.Low);

        modelBuilder.Entity<Ticket>()
            .Property(ticket => ticket.ResolutionDescription)
            .HasColumnType("TEXT");

        modelBuilder.Entity<Ticket>()
            .Property(ticket => ticket.Version)
            .HasDefaultValue(1)
            .IsConcurrencyToken();

        // Restrict users with CreatedTickets from being deleted from the database
        modelBuilder.Entity<Ticket>()
            .HasOne(ticket => ticket.CreatedBy)
            .WithMany(user => user.CreatedTickets)
            .OnDelete(DeleteBehavior.Restrict);

        // Set AssignedTo to null if assigned user is deleted
        modelBuilder.Entity<Ticket>()
            .HasOne(ticket => ticket.AssignedTo)
            .WithMany(user => user.AssignedTickets)
            .OnDelete(DeleteBehavior.SetNull);

        // ================================
        // ==== TICKET HISTORY
        // ================================
        modelBuilder.Entity<TicketHistory>()
            .Property(history => history.Action)
            .HasConversion<string>()
            .HasMaxLength(50);

        modelBuilder.Entity<TicketHistory>()
            .Property(history => history.OldValues)
            .HasColumnType("JSON");

        modelBuilder.Entity<TicketHistory>()
            .Property(history => history.NewValues)
            .HasColumnType("JSON");

        // Restrict users with created TicketHistories from being deleted from the database
        modelBuilder.Entity<TicketHistory>()
            .HasOne(history => history.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        // Automatically delete TicketHistory when its parent Tickey is deleted
        modelBuilder.Entity<TicketHistory>()
            .HasOne(history => history.Ticket)
            .WithMany(ticket => ticket.History)
            .OnDelete(DeleteBehavior.Cascade);

    }
}