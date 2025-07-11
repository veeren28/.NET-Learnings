using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoleBasedAuthentication.Models;
using System.Threading.Tasks;

namespace RoleBasedAuthentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var check = await _signInManager.PasswordSignInAsync(
                model.Name,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (check.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Name
            };

            var check = await _userManager.CreateAsync(user, model.Password);

            if (check.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Login");
            }

            foreach (var error in check.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
    }
}
