
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Task8.Data;
using Task8.Models;

namespace Task8.Controllers
{
    [Authorize]
    public class SupplyRequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public SupplyRequestController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // 1. Display Requests
        public async Task<IActionResult> Index()
        {
            IQueryable<SupplyRequest> requests = _context.SupplyRequests
                .Include(r => r.RequestItems)
                .ThenInclude(i => i.Product);

            // Admin can see all requests
            // Normal user can see only their requests
            if (!User.IsInRole("Admin") &&
                !User.IsInRole("SuperAdmin"))
            {
                var userId = _userManager.GetUserId(User);

                requests = requests.Where(r => r.UserId == userId);
            }

            return View(await requests.ToListAsync());
        }

        // 2. Create - GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _context.Products.ToListAsync();

            return View();
        }

        // 3. Create - POST
        [HttpPost]
        public async Task<IActionResult> Create(List<RequestItem> items)
        {
            if (items == null)
                items = new List<RequestItem>();

            // Get products selected by the user
            var selectedItems = items
                .Where(i => i.ProductId > 0 && i.Quantity > 0)
                .ToList();

            if (selectedItems.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one product.");

                ViewBag.Products = await _context.Products.ToListAsync();

                return View();
            }

            decimal totalPrice = 0;

            var requestItems = new List<RequestItem>();

            foreach (var item in selectedItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);

                if (product == null || item.Quantity > product.Quantity)
                {
                    ModelState.AddModelError("", "Invalid product or quantity.");

                    ViewBag.Products = await _context.Products.ToListAsync();

                    return View();
                }

                var newItem = new RequestItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                requestItems.Add(newItem);

                totalPrice += product.Price * item.Quantity;
            }

            var request = new SupplyRequest
            {
                RequestDate = DateTime.Now,
                TotalPrice = totalPrice,
                Status = "Pending",
                UserId = _userManager.GetUserId(User),
                RequestItems = requestItems
            };

            _context.SupplyRequests.Add(request);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // 4. Details
        public async Task<IActionResult> Details(int id)
        {
            var request = await _context.SupplyRequests
                .Include(r => r.RequestItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound();

            if (!CanAccessRequest(request))
                return Forbid();

            return View(request);
        }

        // 5. Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var request = await _context.SupplyRequests
                .Include(r => r.RequestItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound();

            if (!CanAccessRequest(request))
                return Forbid();

            ViewBag.Products = await _context.Products.ToListAsync();

            return View(request);
        }

        // 6. Edit - POST
        [HttpPost]
        public async Task<IActionResult> Edit(int id, List<RequestItem> items)
        {
            var request = await _context.SupplyRequests
                .Include(r => r.RequestItems)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound();

            if (!CanAccessRequest(request))
                return Forbid();

            if (items == null)
                items = new List<RequestItem>();

            var selectedItems = items
                .Where(i => i.ProductId > 0 && i.Quantity > 0)
                .ToList();

            if (selectedItems.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one product.");

                ViewBag.Products = await _context.Products.ToListAsync();

                return View(request);
            }

            decimal totalPrice = 0;

            var newItems = new List<RequestItem>();

            foreach (var item in selectedItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);

                if (product == null || item.Quantity > product.Quantity)
                {
                    ModelState.AddModelError("", "Invalid product or quantity.");

                    ViewBag.Products = await _context.Products.ToListAsync();

                    return View(request);
                }

                var newItem = new RequestItem
                {
                    SupplyRequestId = request.Id,
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                newItems.Add(newItem);

                totalPrice += product.Price * item.Quantity;
            }

            // Remove old items
            _context.RequestItems.RemoveRange(request.RequestItems);

            // Add new items
            request.RequestItems = newItems;

            request.TotalPrice = totalPrice;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // 7. Delete - GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _context.SupplyRequests
                .Include(r => r.RequestItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound();

            if (!CanAccessRequest(request))
                return Forbid();

            return View(request);
        }

        // 8. Delete - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.SupplyRequests
                .Include(r => r.RequestItems)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return NotFound();

            if (!CanAccessRequest(request))
                return Forbid();

            // Delete request items first
            _context.RequestItems.RemoveRange(request.RequestItems);

            // Delete the request
            _context.SupplyRequests.Remove(request);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Check if the current user can access this request
        private bool CanAccessRequest(SupplyRequest request)
        {
            if (User.IsInRole("Admin") || User.IsInRole("SuperAdmin"))
                return true;

            var userId = _userManager.GetUserId(User);

            return request.UserId == userId;
        }
    }
}
