using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksCollectionsApp.Controllers
{
    public class BookController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

    }
}
