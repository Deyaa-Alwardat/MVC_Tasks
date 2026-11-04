
using Microsoft.AspNetCore.Mvc;

namespace UniversityCafeteria.Controllers
{
    public class SupplyController : Controller
    {
        public IActionResult Index()
        {
            // Create an array of product categories
            string[] categories =
             {
            "Drinks",
            "Food",
            "Snacks",
            "Hot Drinks",
            "Cold Drinks"
           };

            // Pass the array to the View
            ViewBag.Categories = categories;

            return View();
        }
        public IActionResult Products()
        {
            ViewData["Title"]= "Cafeteria Products";
            ViewData["ManagerName"] = "Ahmad Ail";
            ViewData["NumberOfProducts"] = 5;
            string[] products =
            {
                "Water",
                "Orange Juice",
                "Coffee",
                "Sandwich",
                "Chocolate"
            };

            ViewData["Products"]= products;
            return View();

        }

        public IActionResult Suppliers()
        {
            ViewBag.Title = "Our Supplieres";
            ViewBag.NumberOfSuppliers = 3;
            string[] suppliers =
            {
                "Fresh Food Company",
                "Happy Drinks Company",
                "Daily Supplies Company"
            };
            ViewBag.Suppliers = suppliers;
                    return View();
        }
        [HttpGet]
        public IActionResult CreateRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SubmitRequest()
        {
            // Receive data from the form
            string employeeName = Request.Form["EmployeeName"].ToString();
            string productName = Request.Form["ProductName"].ToString();
            string quantity = Request.Form["Quantity"].ToString();
            string requiredDate = Request.Form["RequiredDate"].ToString();

            // Validate Employee Name
            if (string.IsNullOrWhiteSpace(employeeName))
            {
                ViewBag.Message = "Error: Employee Name is required!";
                ViewBag.MessageType = "danger";

                return View();
            }

            // Success message
            ViewBag.Message = "Supply Request Submitted Successfully!";
            ViewBag.MessageType = "success";

            // Send submitted information to the View
            ViewBag.EmployeeName = employeeName;
            ViewBag.ProductName = productName;
            ViewBag.Quantity = quantity;
            ViewBag.RequiredDate = requiredDate;
            TempData["EmployeeName"]= employeeName;

            // Store success message temporarily
            TempData["Message"] = "Supply Request Submitted Successfully!";

            // Redirect to About page
            return RedirectToAction("About");
        }

        public IActionResult About()
        {

            return View();

        }

        public IActionResult CheckQuantity(int quantity)
        {
            if (quantity < 20)
            {
                ViewBag.Message = "Small Order";
            }
            else if (quantity >= 20 && quantity <= 50)
            {
                ViewBag.Message = "Medium Order";
            }
            else
            {
                ViewBag.Message = "Large Order";
            }

            ViewBag.Quantity = quantity;

            return View();
        }


    }
}
