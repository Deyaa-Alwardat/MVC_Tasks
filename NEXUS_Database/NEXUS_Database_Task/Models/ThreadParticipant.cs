using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class ThreadParticipant
{
    public int ThreadId { get; set; }

    public int UserId { get; set; }

    public DateTime? LastReadAt { get; set; }

    public virtual Thread Thread { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
