using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using UniversityCafeteriaSupply.Models;

namespace UniversityCafeteriaSupply.Controllers
{
    public class SupplyRequestController : Controller
    {
        List <SupplyRequestItem> Products=new List<SupplyRequestItem> ();
        static List<ConfirmedSupplyRequest> confirmedRequests =
    new List<ConfirmedSupplyRequest>();
        static int nextRequestId = 1;
        public void SaveProducts(List<SupplyRequestItem> products)
        {
            string json=JsonSerializer.Serialize(products);
            HttpContext.Session.SetString("products", json);
        }
        private List<SupplyRequestItem> GetProducts()
        {
            var json = HttpContext.Session.GetString("products");
            if (json == null)
            {
                return new List<SupplyRequestItem>();
            }
            return JsonSerializer.Deserialize<List<SupplyRequestItem>>(json)
           ?? new List<SupplyRequestItem>();
        }
        public IActionResult Index()
        {

            List<SupplyRequestItem> products = GetProducts();
            ViewBag.PreferredPriority = Request.Cookies["PreferredPriority"];

            return View(products);

           
        }
      

        [HttpPost]
        public IActionResult AddItem(SupplyRequestItem item)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(item.ProductName))
            {
                errors.Add("Product Name is required");
            }

            if (string.IsNullOrWhiteSpace(item.Category))
            {
                errors.Add("Category is required");
            }

            if (item.Quantity <= 0)
            {
                errors.Add("Quantity must be greater than zero");
            }

            if (item.UnitPrice <= 0)
            {
                errors.Add("Unit Price must be greater than zero");
            }

            if (errors.Count > 0)
            {
                ViewBag.Errors = errors;
                return View("Index");
            }

            List<SupplyRequestItem> Products = GetProducts();
            item.Total = item.Quantity * item.UnitPrice;
            Products.Add(item);
            SaveProducts(Products);
            return RedirectToAction("Index");
        }


        public IActionResult Review()
        {
            List<SupplyRequestItem> products = GetProducts();
            ViewBag.Products = products;
            decimal subtotal = 0;
            foreach (var product in products)
            {
                subtotal+= product.Total;
            }
            ViewBag.Subtotal=subtotal;

            decimal preparationFee = 0;

            foreach (var product in products)
            {
                switch (product.Priority)
                {
                    case "Normal":
                        preparationFee += 2;
                        break;

                    case "Urgent":
                        preparationFee += 5;
                        break;

                    case "Emergency":
                        preparationFee += 10;
                        break;
                }
            }

            ViewBag.PreparationFee = preparationFee;

            decimal discount = 0;

            if (subtotal > 200)
            {
                discount = subtotal * 0.15m;
            }
            else if (subtotal > 100)
            {
                discount = subtotal * 0.10m;
            }

            ViewBag.Discount = discount;

            decimal finalTotal = subtotal + preparationFee - discount;

            ViewBag.FinalTotal = finalTotal;
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult ConfirmRequest()
        {
            // 1. Get products from Session
            List<SupplyRequestItem> products = GetProducts();

            // 2. Check if there are products
            if (products.Count == 0)
            {
                return RedirectToAction("Index");
            }

            // 3. Calculate Subtotal
            decimal subtotal = 0;

            foreach (var product in products)
            {
                subtotal += product.Total;
            }

            // 4. Calculate Preparation Fee
            decimal preparationFee = 0;

            foreach (var product in products)
            {
                switch (product.Priority)
                {
                    case "Normal":
                        preparationFee += 2;
                        break;

                    case "Urgent":
                        preparationFee += 5;
                        break;

                    case "Emergency":
                        preparationFee += 10;
                        break;
                }
            }

            // 5. Calculate Discount
            decimal discount = 0;

            if (subtotal > 200)
            {
                discount = subtotal * 0.15m;
            }
            else if (subtotal > 100)
            {
                discount = subtotal * 0.10m;
            }

            // 6. Calculate Final Total
            decimal finalTotal = subtotal + preparationFee - discount;

            // 7. Create a new confirmed request
            ConfirmedSupplyRequest request = new ConfirmedSupplyRequest();

            request.RequestId = nextRequestId;
            request.Products = products;
            request.Subtotal = subtotal;
            request.PreparationFee = preparationFee;
            request.Discount = discount;
            request.FinalTotal = finalTotal;
            request.Status = "Confirmed";

            // 8. Save the request
            confirmedRequests.Add(request);

            // 9. Increase Request ID
            nextRequestId++;

            // 10. Clear the draft from Session
            HttpContext.Session.Remove("Products");

            // 11. Go to My Requests page
            return RedirectToAction("MyRequests");
        }

        [HttpPost]
        public IActionResult ClearDraft()
        {
            HttpContext.Session.Remove("Products");

            return RedirectToAction("Index");
        }
        public IActionResult SavePreference(string priority)
        {
            CookieOptions options=new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);

            Response.Cookies.Append("PreferredPriority", priority, options);
            TempData["PreferenceMessage"]= "Preferred priority saved successfully.";
            return RedirectToAction("Index");
        }

        public IActionResult MyRequests()
        {
            return View(confirmedRequests);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
