using System.Diagnostics;
using EntityFwDay1.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFwDay1.Controllers
{
    public class HomeController : Controller
    {
  
        private readonly  AppContextDb _context;
        public HomeController(AppContextDb context)
        {
            _context = context;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            _context.students.Add(new Student() { Name = "Veeren" });
            _context.SaveChanges();
            var st = _context.students.ToList();
            return View(st);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
