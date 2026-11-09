using Microsoft.AspNetCore.Mvc;
using UniversityCafeteriaSystem.Models;
using Microsoft.EntityFrameworkCore;
using UniversityCafeteriaSystem.Data;

namespace UniversityCafeteriaSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var employee = _context.Employee.ToList();
            var employee = _context.Employees
                                    .Include(e => e.Orders)
                                    .ToList();

            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {

            _context.Employees.Add(employee);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit( int id )
        {
            var employee=_context.Employees.Find(id);
            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {

            _context.Employees.Update(employee);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = _context.Employees.Find(id);
            return View(employee);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);

            _context.Employees.Remove(employee);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        

    }
}
