using AuthtenticationPract.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AuthtenticationPract.Controllers
{
    public class RegistrationController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        public RegistrationController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new IdentityUser
            {
                UserName = model.Name
            };
            var check= await _userManager.CreateAsync(user, model.Password);
            if (check.Succeeded) {
                return RedirectToAction("Login", "Account");
                    }
            foreach(var error in check.Errors)
            {
                ModelState.AddModelError(" ", error.Description);
            }
            return View(model);
        }
    }
}
