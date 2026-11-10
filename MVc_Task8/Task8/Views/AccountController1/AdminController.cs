
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Task8.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // 1. Display all users
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            var userRoles = new Dictionary<string, string>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userRoles[user.Id] = string.Join(", ", roles);
            }

            ViewBag.UserRoles = userRoles;

            return View(users);
        }

        // 2. Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            ViewBag.Roles = _roleManager.Roles.ToList();

            var userRoles = await _userManager.GetRolesAsync(user);

            ViewBag.SelectedRole = userRoles.FirstOrDefault();

            return View(user);
        }

        // 3. Edit - POST
        [HttpPost]
        public async Task<IActionResult> Edit(string id, string roleName)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                ModelState.AddModelError("", "Role does not exist.");

                ViewBag.Roles = _roleManager.Roles.ToList();

                return View(user);
            }

            // Get the user's current roles
            var oldRoles = await _userManager.GetRolesAsync(user);

            // Remove old roles
            await _userManager.RemoveFromRolesAsync(user, oldRoles);

            // Add the new role
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Could not change user role.");

            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(user);
        }

        // 4. Delete user
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
    }
}
