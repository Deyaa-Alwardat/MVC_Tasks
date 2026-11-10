using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task8.Models;

namespace Task8.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<SupplyRequest> SupplyRequests { get; set; }

        public DbSet<RequestItem> RequestItems { get; set; }
    }
}
