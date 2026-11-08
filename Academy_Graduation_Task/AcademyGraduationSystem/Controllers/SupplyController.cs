using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AcademyGraduationSystem.Controllers
{
    public class SupplyController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult  Create(string CertificateOffer, string HospitalityOffer, string GiftOffer)
        {
            if (string.IsNullOrEmpty(CertificateOffer) ||
                string.IsNullOrEmpty(HospitalityOffer) ||
                string.IsNullOrEmpty(GiftOffer))
                        {
                ViewBag.Error = "Please select one offer from each category.";
                return View();
            }
            //ViewBag.CertificateOffer = CertificateOffer;
            //ViewBag.HospitalityOffer = HospitalityOffer;
            //ViewBag.GiftOffer = GiftOffer;

            HttpContext.Session.SetString("CertificateOffer", CertificateOffer);
            HttpContext.Session.SetString("HospitalityOffer", HospitalityOffer);
            HttpContext.Session.SetString("GiftOffer", GiftOffer);

            List<string> selectedOffers = new List<string>();
            selectedOffers.Add(CertificateOffer);
            selectedOffers.Add(HospitalityOffer);
            selectedOffers.Add(GiftOffer);

            string offersJson = JsonSerializer.Serialize(selectedOffers);
            HttpContext.Session.SetString("SelectedOffers", offersJson);


            return RedirectToAction("Calculate");
        }

        [HttpGet]
        public IActionResult CheckSession()
        {
            ViewBag.CertificateOffer =
                HttpContext.Session.GetString("CertificateOffer");

            ViewBag.HospitalityOffer =
                HttpContext.Session.GetString("HospitalityOffer");

            ViewBag.GiftOffer =
                HttpContext.Session.GetString("GiftOffer");

            return View();
        }
        public IActionResult Calculate() {
            int quantity = 0;
            decimal unitPrice = 0;
            

            string certificate = HttpContext.Session.GetString("CertificateOffer");

            string hospitality =HttpContext.Session.GetString("HospitalityOffer");
               
            string gift =HttpContext.Session.GetString("GiftOffer");

            
            int certificateQuantity = 0;
            decimal certificateUnitPrice = 0;
            switch (certificate)
            {
                case "A":
                    certificateQuantity = 35;
                    certificateUnitPrice = 2;
                    break;

                case "B":
                    certificateQuantity = 35;
                    certificateUnitPrice = 1.5m;
                    break;

                case "C":
                    certificateQuantity = 35;
                    certificateUnitPrice = 2.5m;
                    break;
            }
            decimal certificateTotal = certificateQuantity * certificateUnitPrice;
            HttpContext.Session.SetString("CertificateQuantity", certificateQuantity.ToString());

            HttpContext.Session.SetString("CertificateUnitPrice", certificateUnitPrice.ToString());

            HttpContext.Session.SetString("CertificateTotal", certificateTotal.ToString());

            int hospitalityQuantity = 0;
            decimal hospitalityUnitPrice = 0;
            switch (hospitality)
            {
                case "D":
                    hospitalityQuantity = 100;
                    hospitalityUnitPrice = 2;
                    break;

                case "E":
                    hospitalityQuantity = 100;
                    hospitalityUnitPrice = 3;
                    break;

                case "F":
                    hospitalityQuantity = 100;
                    hospitalityUnitPrice = 4;
                    break;
            }
            decimal hospitalityTotal =hospitalityQuantity * hospitalityUnitPrice;
            HttpContext.Session.SetString("HospitalityQuantity", hospitalityQuantity.ToString());

            HttpContext.Session.SetString("HospitalityUnitPrice", hospitalityUnitPrice.ToString());

            HttpContext.Session.SetString("HospitalityTotal", hospitalityTotal.ToString());

            int giftQuantity = 0;
            decimal giftUnitPrice = 0;
            switch (gift)
            {
                case "G":
                    giftQuantity = 35;
                    giftUnitPrice = 2;
                    break;

                case "H":
                    giftQuantity = 35;
                    giftUnitPrice = 3;
                    break;

                case "I":
                    giftQuantity = 35;
                    giftUnitPrice = 4;
                    break;
            }
            decimal giftTotal = giftQuantity * giftUnitPrice;
            HttpContext.Session.SetString("GiftQuantity", giftQuantity.ToString());

            HttpContext.Session.SetString("GiftUnitPrice", giftUnitPrice.ToString());

            HttpContext.Session.SetString("GiftTotal", giftTotal.ToString());

            decimal totalPrice = certificateTotal + hospitalityTotal + giftTotal;
            decimal discountPercentage = 0;

            if (totalPrice > 500)
            {
                discountPercentage = 0.15m;
            }
            else if (totalPrice > 300)
            {
                discountPercentage = 0.10m;
            }
            else
            {
                discountPercentage = 0;
            }
            decimal discountAmount = totalPrice * discountPercentage;
            decimal finalTotal = totalPrice - discountAmount;
            HttpContext.Session.SetString("TotalPrice", totalPrice.ToString());
            HttpContext.Session.SetString("DiscountPercentage", (discountPercentage * 100).ToString());
            HttpContext.Session.SetString("DiscountAmount", discountAmount.ToString());
            HttpContext.Session.SetString("FinalTotal", finalTotal.ToString());

            

            ViewBag.TotalPrice = totalPrice;
            ViewBag.DiscountPercentage = discountPercentage * 100;
            ViewBag.DiscountAmount = discountAmount;
            ViewBag.FinalTotal = finalTotal;

            return RedirectToAction("Review");
        }
        [HttpGet]
        public IActionResult Review()
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("CertificateOffer")) ||
                    string.IsNullOrEmpty(HttpContext.Session.GetString("HospitalityOffer")) ||
                    string.IsNullOrEmpty(HttpContext.Session.GetString("GiftOffer")))
            {
                return RedirectToAction("Create");
            }
            // Selected Offers
            ViewBag.CertificateOffer = HttpContext.Session.GetString("CertificateOffer");
            ViewBag.HospitalityOffer = HttpContext.Session.GetString("HospitalityOffer");
            ViewBag.GiftOffer = HttpContext.Session.GetString("GiftOffer");
            switch (ViewBag.CertificateOffer)
            {
                case "A":
                    ViewBag.CertificateName = "Supplier A - Premium Certificate Package";
                    break;
                case "B":
                    ViewBag.CertificateName = "Supplier B - Standard Certificate Package";
                    break;
                case "C":
                    ViewBag.CertificateName = "Supplier C - Luxury Certificate Package";
                    break;
            }

            switch (ViewBag.HospitalityOffer)
            {
                case "D":
                    ViewBag.HospitalityName = "Supplier D - Coffee & Drinks Package";
                    break;
                case "E":
                    ViewBag.HospitalityName = "Supplier E - Sandwiches & Snacks Package";
                    break;
                case "F":
                    ViewBag.HospitalityName = "Supplier F - Full Hospitality Package";
                    break;
            }

            switch (ViewBag.GiftOffer)
            {
                case "G":
                    ViewBag.GiftName = "Supplier G - Customized Mugs Package";
                    break;
                case "H":
                    ViewBag.GiftName = "Supplier H - Graduation Bags Package";
                    break;
                case "I":
                    ViewBag.GiftName = "Supplier I - Premium Gift Boxes Package";
                    break;
            }

            // Certificate Details
            ViewBag.CertificateQuantity = HttpContext.Session.GetString("CertificateQuantity");
            ViewBag.CertificateUnitPrice = HttpContext.Session.GetString("CertificateUnitPrice");
            ViewBag.CertificateTotal = HttpContext.Session.GetString("CertificateTotal");

            // Hospitality Details
            ViewBag.HospitalityQuantity = HttpContext.Session.GetString("HospitalityQuantity");
            ViewBag.HospitalityUnitPrice = HttpContext.Session.GetString("HospitalityUnitPrice");
            ViewBag.HospitalityTotal = HttpContext.Session.GetString("HospitalityTotal");

            // Gift Details
            ViewBag.GiftQuantity = HttpContext.Session.GetString("GiftQuantity");
            ViewBag.GiftUnitPrice = HttpContext.Session.GetString("GiftUnitPrice");
            ViewBag.GiftTotal = HttpContext.Session.GetString("GiftTotal");

            // Cost Summary
            ViewBag.TotalPrice = HttpContext.Session.GetString("TotalPrice");
            ViewBag.DiscountPercentage = HttpContext.Session.GetString("DiscountPercentage");
            ViewBag.DiscountAmount = HttpContext.Session.GetString("DiscountAmount");
            ViewBag.FinalTotal = HttpContext.Session.GetString("FinalTotal");

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
