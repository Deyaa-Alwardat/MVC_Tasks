using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Message
{
    public int Id { get; set; }

    public int ThreadId { get; set; }

    public int SenderId { get; set; }

    public string Body { get; set; } = null!;

    public DateTime SentAt { get; set; }

    public virtual User Sender { get; set; } = null!;

    public virtual Thread Thread { get; set; } = null!;
}
