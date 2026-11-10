using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Expert
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? HomeSpaceId { get; set; }

    public string Title { get; set; } = null!;

    public string? Bio { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal? RatingAvg { get; set; }

    public int SessionsCount { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual ICollection<ExpertAvailability> ExpertAvailabilities { get; set; } = new List<ExpertAvailability>();

    public virtual ICollection<FavoriteExpert> FavoriteExperts { get; set; } = new List<FavoriteExpert>();

    public virtual Space? HomeSpace { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
