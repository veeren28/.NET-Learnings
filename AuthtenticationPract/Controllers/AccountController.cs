using AuthtenticationPract.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthtenticationPract.Controllers
{
    public class AccountController:Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController (SignInManager<IdentityUser> signInManager)
        {
            _signInManager= signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var check = await _signInManager.PasswordSignInAsync(
                model.Name,
                model.Password,
                isPersistent: false,
                 lockoutOnFailure: false


                );
                if (check.Succeeded)
            {
                return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError("", "Invalid User or Password");
                return View(model);

            
            ;
        }
    }
}
