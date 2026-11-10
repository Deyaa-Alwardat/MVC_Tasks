using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class SupportTicket
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Category { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Reply { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
