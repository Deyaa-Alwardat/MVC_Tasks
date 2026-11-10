using System;
using System.Collections.Generic;

namespace NEXUS_Database_Task.Models;

public partial class Coupon
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Code { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string DiscountType { get; set; } = null!;

    public decimal DiscountValue { get; set; }

    public string AppliesTo { get; set; } = null!;

    public DateOnly? ExpiresAt { get; set; }

    public DateTime? UsedAt { get; set; }

    public virtual User? User { get; set; }
}
