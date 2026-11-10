using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Task8.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();

            ViewBag.Users = users;
            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Assign(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var role = await _roleManager.FindByIdAsync(roleId);

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
