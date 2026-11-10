using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? CompanyId { get; set; }

    public int SpaceId { get; set; }

    public DateOnly BookingDate { get; set; }

    public string DurationType { get; set; } = null!;

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public int? People { get; set; }

    public string Status { get; set; } = null!;

    public decimal BaseFee { get; set; }

    public decimal ServiceFee { get; set; }

    public decimal AddonsFee { get; set; }

    public decimal TotalFee { get; set; }

    public decimal OwnerCommission { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public decimal CancelPenalty { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime? PayBy { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();

    public virtual Company? Company { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Review? Review { get; set; }

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

    public virtual Space Space { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
}
