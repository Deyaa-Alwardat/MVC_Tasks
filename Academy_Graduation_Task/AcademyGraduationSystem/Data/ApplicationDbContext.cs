using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AcademyGraduationSystem.Models;

namespace AcademyGraduationSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<SupplyRequest> SupplyRequests { get; set; }

        public DbSet<SupplyRequestItem> SupplyRequestItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Offer>()
                .Property(o => o.UnitPrice)
                .HasPrecision(18, 2);

            builder.Entity<SupplyRequest>()
                .Property(s => s.TotalPrice)
                .HasPrecision(18, 2);

            builder.Entity<SupplyRequest>()
                .Property(s => s.DiscountPercentage)
                .HasPrecision(5, 2);

            builder.Entity<SupplyRequest>()
                .Property(s => s.DiscountAmount)
                .HasPrecision(18, 2);

            builder.Entity<SupplyRequest>()
                .Property(s => s.FinalTotal)
                .HasPrecision(18, 2);

            builder.Entity<SupplyRequestItem>()
                .Property(i => i.UnitPrice)
                .HasPrecision(18, 2);

            builder.Entity<SupplyRequestItem>()
                .Property(i => i.TotalPrice)
                .HasPrecision(18, 2);
        }

    }
}