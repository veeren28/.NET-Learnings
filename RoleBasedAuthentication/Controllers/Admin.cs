using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuthentication.Controllers
{
    public class Admin : Controller
    {
        public IActionResult AdminOnly()
        {
            return View();
        }
    }
}
