using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuthentication.Controllers
{
    [Authorize (Roles="Student")]
    public class Student : Controller
    {
        public IActionResult StudentOnly()
        {
            return View();
        }
    }
}
