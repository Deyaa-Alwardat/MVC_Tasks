using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Amenity
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Label { get; set; } = null!;

    public virtual ICollection<Space> Spaces { get; set; } = new List<Space>();
}
