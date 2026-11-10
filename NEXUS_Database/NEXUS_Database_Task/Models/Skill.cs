using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Expert> Experts { get; set; } = new List<Expert>();
}
