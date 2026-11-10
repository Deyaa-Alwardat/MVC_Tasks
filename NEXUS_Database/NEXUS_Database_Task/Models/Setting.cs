using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Setting
{
    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
