using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class SpaceService
{
    public int SpaceId { get; set; }

    public int ServiceId { get; set; }

    public decimal Price { get; set; }

    public virtual Service Service { get; set; } = null!;

    public virtual Space Space { get; set; } = null!;
}
