using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class ServiceRequest
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int UnitId { get; set; }

    public int ServiceId { get; set; }

    public string? Note { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;

    public virtual Unit Unit { get; set; } = null!;
}
