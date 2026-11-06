using System;
using System.Collections.Generic;
using CafeteriaTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaTask.Data;

public partial class CafeteriaDbContext : DbContext
{
    public CafeteriaDbContext()
    {
    }

    public CafeteriaDbContext(DbContextOptions<CafeteriaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=CafeteriaDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC0771E39D57");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07B58115C7");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("FK__Products__Catego__4F7CD00D");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products).HasConstraintName("FK__Products__Suppli__5070F446");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Purchase__3214EC07D56B98B2");

            entity.HasOne(d => d.Supplier).WithMany(p => p.PurchaseOrders).HasConstraintName("FK__PurchaseO__Suppl__5535A963");
        });

        modelBuilder.Entity<PurchaseOrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Purchase__3214EC07EC6C88C6");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseOrderItems).HasConstraintName("FK__PurchaseO__Produ__59FA5E80");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.PurchaseOrderItems).HasConstraintName("FK__PurchaseO__Purch__59063A47");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supplier__3214EC0749C584AD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
