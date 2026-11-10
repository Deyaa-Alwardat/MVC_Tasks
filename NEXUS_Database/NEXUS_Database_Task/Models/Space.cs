using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Space
{
    public int Id { get; set; }

    public int OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public string Area { get; set; } = null!;

    public string City { get; set; } = null!;

    public decimal DailyPrice { get; set; }

    public decimal HourlyPrice { get; set; }

    public bool InstantApproval { get; set; }

    public decimal? RatingAvg { get; set; }

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual ICollection<ExpertAvailability> ExpertAvailabilities { get; set; } = new List<ExpertAvailability>();

    public virtual ICollection<Expert> Experts { get; set; } = new List<Expert>();

    public virtual ICollection<FavoriteSpace> FavoriteSpaces { get; set; } = new List<FavoriteSpace>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<SpaceService> SpaceServices { get; set; } = new List<SpaceService>();

    public virtual ICollection<Thread> Threads { get; set; } = new List<Thread>();

    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();

    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
}
