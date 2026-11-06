using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UniversityCafeteria.Models;

namespace UniversityCafeteria.Controllers
{
    public class SupplyRequestController : Controller
    {
        public IActionResult Index()
        {
            // البحث عن الكوكي المحفوظ في متصفح المستخدم
            string savedPriority = Request.Cookies["PreferredPriority"];

            // إذا وجدنا الكوكي (الموظف قام بحفظ تفضيله سابقاً)
            if (!string.IsNullOrEmpty(savedPriority))
            {
                // نرسل الرسالة المطلوبة لصفحة العرض
                ViewBag.Message = $"Your preferred request priority is {savedPriority}.";

                // نرسل القيمة المحفوظة لنجعلها الخيار المحدد تلقائياً في القائمة المنسدلة
                ViewBag.DefaultPriority = savedPriority;
            }
            else
            {
                // إذا لم يكن هناك كوكي (أول زيارة للموظف)، نضع "Normal" كقيمة افتراضية
                ViewBag.DefaultPriority = "Normal";
            }

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


            // Get existing products from Session
            string existingItems = HttpContext.Session.GetString("Items");
            List<SupplyRequestItem> items;
            if (existingItems == null)
            {
                items = new List<SupplyRequestItem>();
            }
            else
            {
                items = JsonSerializer.Deserialize<List<SupplyRequestItem>>(existingItems);
            }
            // Create new product
            SupplyRequestItem newItem = new SupplyRequestItem();

            newItem.ProductName = ProductName;
            newItem.Category = Category;
            newItem.Quantity = Quantity;
            newItem.UnitPrice = UnitPrice;

            // Add product to the list
            items.Add(newItem);
            // Save updated list in Session
            string jsonItems = JsonSerializer.Serialize(items);

            HttpContext.Session.SetString("Items", jsonItems);

            // Save Priority
            HttpContext.Session.SetString("Priority", Priority);
            return RedirectToAction("Index");
        }
        public IActionResult Review()
        {
            // Get products from Session
            string existingItems = HttpContext.Session.GetString("Items");

            List<SupplyRequestItem> items = new List<SupplyRequestItem>();

            if (existingItems != null)
            {
                items = JsonSerializer.Deserialize<List<SupplyRequestItem>>(existingItems);
            }

            // Calculate Subtotal
            decimal subtotal = 0;

            foreach (SupplyRequestItem item in items)
            {
                subtotal += item.Quantity * item.UnitPrice;
            }

            // Get Priority
            string Priority = HttpContext.Session.GetString("Priority");

            // Calculate Preparation Fee
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

            // Calculate Discount
            decimal discount = 0;

            if (subtotal > 200)
            {
                discount = subtotal * 0.15m;
            }
            else if (subtotal > 100)
            {
                discount = subtotal * 0.10m;
            }

            // Calculate Final Total
            decimal finalTotal = subtotal + preparationFee - discount;

            return View(items);
        }
        public IActionResult ConfirmRequest()
        {
            // مسح بيانات المسودة من الـ Session بعد تأكيد الطلب بنجاح
            HttpContext.Session.Remove("Items");
            HttpContext.Session.Remove("Priority");

            // توجيه الموظف لصفحة "طلباتي" (سنبنيها في الجزء السادس)
            return RedirectToAction("MyRequests");
            return View();
        }
        public IActionResult ClearDraft()
        {
            // مسح المنتجات المحفوظة
            HttpContext.Session.Remove("Items");

            // مسح الأولوية المحفوظة
            HttpContext.Session.Remove("Priority");

            // إعادة توجيه المستخدم لصفحة الإضافة الرئيسية
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SavePreference(string Priority)
        {
            CookieOptions options = new CookieOptions();
            options.Expires= DateTime.Now.AddDays(7);
            Response.Cookies.Append("PreferredPriority", Priority, options);
            return RedirectToAction("Index");
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
