using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class User
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? Phone { get; set; }

    public string? City { get; set; }

    public string? Bio { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Company? Company { get; set; }

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

    public virtual Expert? Expert { get; set; }

    public virtual ICollection<FavoriteExpert> FavoriteExperts { get; set; } = new List<FavoriteExpert>();

    public virtual ICollection<FavoriteSpace> FavoriteSpaces { get; set; } = new List<FavoriteSpace>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Payout> Payouts { get; set; } = new List<Payout>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Space> Spaces { get; set; } = new List<Space>();

    public virtual ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();

    public virtual ICollection<ThreadParticipant> ThreadParticipants { get; set; } = new List<ThreadParticipant>();

    public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
}
