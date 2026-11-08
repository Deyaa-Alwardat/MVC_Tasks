using Microsoft.AspNetCore.Mvc;
using UniversityCafeteriaSystem.Data;
using UniversityCafeteriaSystem.Models;

namespace UniversityCafeteriaSystem.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SupplierController (ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var supplires= _context.Suppliers.ToList();
            return View(supplires);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}
