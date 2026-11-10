using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class WalletTransaction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? BookingId { get; set; }

    public int? ConsultationId { get; set; }

    public string Type { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Consultation? Consultation { get; set; }

    public virtual User User { get; set; } = null!;
}
