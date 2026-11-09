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

        // GET: Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Update(supplier);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var supplier = _context.Suppliers.Find(id);

            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
