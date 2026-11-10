using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class AuditLog
{
    public int Id { get; set; }

    public int? ActorId { get; set; }

    public string Action { get; set; } = null!;

    public string? Detail { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? Actor { get; set; }
}
