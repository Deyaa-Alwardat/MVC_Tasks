using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class ExpertAvailability
{
    public int Id { get; set; }

    public int ExpertId { get; set; }

    public int? SpaceId { get; set; }

    public byte Weekday { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public string Mode { get; set; } = null!;

    public virtual Expert Expert { get; set; } = null!;

    public virtual Space? Space { get; set; }
}
