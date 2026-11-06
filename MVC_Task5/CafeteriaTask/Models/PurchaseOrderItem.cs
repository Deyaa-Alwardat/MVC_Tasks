using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaTask.Models;

public partial class PurchaseOrderItem
{
    [Key]
    public int Id { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal UnitPrice { get; set; }

    public int? PurchaseOrderId { get; set; }

    public int? ProductId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("PurchaseOrderItems")]
    public virtual Product? Product { get; set; }

    [ForeignKey("PurchaseOrderId")]
    [InverseProperty("PurchaseOrderItems")]
    public virtual PurchaseOrder? PurchaseOrder { get; set; }
}
