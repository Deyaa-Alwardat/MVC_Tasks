
using CafeteriaTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace CafeteriaTask.Data
{
    public class CafeteriaEmployeesDbContext :DbContext
    {
        public DbSet<Category> Categories {  get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public CafeteriaEmployeesDbContext(
            DbContextOptions<CafeteriaEmployeesDbContext> options)
            : base(options)
        {
        }
    }
}
