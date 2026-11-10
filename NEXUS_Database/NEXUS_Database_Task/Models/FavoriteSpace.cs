using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class FavoriteSpace
{
    public int UserId { get; set; }

    public int SpaceId { get; set; }

    public DateTime SavedAt { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
