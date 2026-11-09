
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityCafeteriaSystem.Data;
using UniversityCafeteriaSystem.Models;

namespace UniversityCafeteriaSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index: Display all orders
        public IActionResult Index()
        {
            var orders = _context.Orders
                                 .Include(o => o.Employee)
                                 .ToList();

            return View(orders);
        }

        // Create (GET): Display form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create (POST): Save new order
        [HttpPost]
        public IActionResult Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Edit (GET): Display selected order
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        // Edit (POST): Update selected order
        [HttpPost]
        public IActionResult Edit(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Details: Display order details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = _context.Orders
                                .Include(o => o.Employee)
                                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        // Delete: Delete selected order
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
