using AspNetCoreGeneratedDocument;
using EntityFrameWorkPract.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameWorkPract.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _UserManager;
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> UserManager)
        {
            _signInManager = signInManager;
            _UserManager = UserManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) { return View(model); }
            var result =  await _signInManager.PasswordSignInAsync(model.Username, model.Password,false,false);
            if (result.Succeeded) {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "username is empty");
            return View(model);
        }
    }
}

       
