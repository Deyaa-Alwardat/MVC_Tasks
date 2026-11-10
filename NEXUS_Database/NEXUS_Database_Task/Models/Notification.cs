using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? BookingId { get; set; }

    public int? ConsultationId { get; set; }

    public string Type { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string? Href { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Consultation? Consultation { get; set; }

    public virtual User User { get; set; } = null!;
}
