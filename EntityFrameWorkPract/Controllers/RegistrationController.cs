using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EntityFrameWorkPract.Models;

namespace EntityFrameWorkPract.Controllers
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
            return View("Registration");
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); ;
            }
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var check = await _userManager.CreateAsync(user, model.Password);
            if (check.Succeeded)
            {
                return RedirectToAction("Login", "Account");


            }
            foreach (var i in check.Errors)
            {
                ModelState.AddModelError(" ", i.Description);
            }
            return View("Registration",model);

        }
    }
}
