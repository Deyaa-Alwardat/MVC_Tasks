using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Review
{
    public int Id { get; set; }

    public int AuthorId { get; set; }

    public int? BookingId { get; set; }

    public int? ConsultationId { get; set; }

    public byte Rating { get; set; }

    public string? Tags { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Booking? Booking { get; set; }

    public virtual Consultation? Consultation { get; set; }
}
