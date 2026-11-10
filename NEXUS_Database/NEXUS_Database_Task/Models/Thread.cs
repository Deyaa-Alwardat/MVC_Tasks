using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Thread
{
    public int Id { get; set; }

    public int? SpaceId { get; set; }

    public string Subject { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Space? Space { get; set; }

    public virtual ICollection<ThreadParticipant> ThreadParticipants { get; set; } = new List<ThreadParticipant>();
}
