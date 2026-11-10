
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task8.Models;

namespace Task8.Controllers
{
    public class AccountController1 : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController1(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            var newUser = new IdentityUser
            {
                UserName = register.Email,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber
            };

            var result = await _userManager.CreateAsync(newUser, register.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(register);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var result = await _signInManager.PasswordSignInAsync(
                login.Email,
                login.Password,
                login.RememberMe,
                false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Account is not allowed to login.");
            }
            else
            {
                ModelState.AddModelError("", "Email or Password is incorrect.");
            }

            return View(login);
        }
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> LogoutConfirmed()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }
    }

}
