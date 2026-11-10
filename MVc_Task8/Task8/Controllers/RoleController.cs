using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Task8.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles=_roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var obj = new IdentityRole(roleName);

            var result = await _roleManager.CreateAsync(obj);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
