using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Service
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();

    public virtual ICollection<SpaceService> SpaceServices { get; set; } = new List<SpaceService>();
}
