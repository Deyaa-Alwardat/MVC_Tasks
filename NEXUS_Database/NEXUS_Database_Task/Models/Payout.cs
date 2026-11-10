using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Payout
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public string Method { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Note { get; set; }

    public DateTime RequestedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
