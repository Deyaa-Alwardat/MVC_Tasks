using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Consultation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? CompanyId { get; set; }

    public int ExpertId { get; set; }

    public int? SpaceId { get; set; }

    public DateOnly SessionDate { get; set; }

    public TimeOnly SessionTime { get; set; }

    public string Mode { get; set; } = null!;

    public string Topic { get; set; } = null!;

    public string Status { get; set; } = null!;

    public decimal BaseFee { get; set; }

    public decimal ServiceFee { get; set; }

    public decimal ExpertCommission { get; set; }

    public decimal TotalFee { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime? PayBy { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Company? Company { get; set; }

    public virtual Expert Expert { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual Review? Review { get; set; }

    public virtual Space? Space { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
}
