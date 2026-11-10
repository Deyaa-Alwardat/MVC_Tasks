using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Unit
{
    public int Id { get; set; }

    public int SpaceId { get; set; }

    public string Code { get; set; } = null!;

    public string UnitType { get; set; } = null!;

    public string? Zone { get; set; }

    public int Capacity { get; set; }

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

    public virtual Space Space { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
