using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Industry { get; set; }

    public string? SizeRange { get; set; }

    public string? City { get; set; }

    public string? TaxId { get; set; }

    public string? Website { get; set; }

    public decimal MonthlyBudget { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
