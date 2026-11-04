using Microsoft.AspNetCore.Mvc;

namespace UniversityCafeteria.Controllers
{
    public class SupplyRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddItem(
                string ProductName,
                string Category,
                int Quantity,
                decimal UnitPrice,
                string Priority)
        {
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                ViewBag.ErrorMessage = "Product Name is required";
                return View("Index");
            }
            if (Quantity <= 0)
            {
                ViewBag.ErrorMessage = "Quantity must be greater than zero";
                return View("Index");
            }
            if (UnitPrice <= 0)
            {
                ViewBag.ErrorMessage = "Unit Price must be greater than zero";
                return View("Index");
            }


            HttpContext.Session.SetString("Priority", Priority);
            return RedirectToAction("Index");
        }
        public IActionResult Review()
        {
            string Priority = HttpContext.Session.GetString("Priority");
            // 1. Calculate Subtotal
            decimal subtotal = 0;

            // 2. Calculate each product total
            // Product Total = Quantity * UnitPrice
            // 3. Add all product totals to Subtotal
            // 4. Calculate Preparation Fee
            decimal preparationFee = 0;
            switch (Priority)
            {
                case "Normal":
                    preparationFee = 2;
                    break;
                case "Urgent":
                    preparationFee = 5;
                    break;
                case "Emergency":
                    preparationFee = 10;
                    break;
            }
           // 5.Calculate Discount
            decimal discount = 0;

            if (subtotal > 200)
            {
                discount = subtotal * 0.15m;
            }
            else if (subtotal > 100)
            {
                discount = subtotal * 0.10m;
            }
            // 6.Calculate Final Total
                decimal finalTotal = subtotal + preparationFee - discount;

            return View();
        }
        public IActionResult ConfirmRequest()
        {
            return View();
        }
        public IActionResult ClearDraft()
        {
            return View();
        }
        public IActionResult SavePreference()
        {
            return View();
        }
        public IActionResult MyRequests()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        
    }
}
