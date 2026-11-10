using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class BookingService
{
    public int BookingId { get; set; }

    public int ServiceId { get; set; }

    public decimal Price { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;
}
