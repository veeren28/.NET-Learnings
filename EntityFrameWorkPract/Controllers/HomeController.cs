using System.Diagnostics;
using EntityFrameWorkPract.Models;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameWorkPract.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppContextDb _context;

        public HomeController(ILogger<HomeController> logger,AppContextDb contextDb)
        {
            _logger = logger;
            _context = contextDb;
        }
        [HttpGet]
        public IActionResult Index()
        {
           
            var st = _context.Students.ToList();
            if (st.Count == 0)
            {
                ViewBag.Msg = "List is Empty Add Students";
            }
            else
            {
                ViewBag.Msg=null;
            }
            return View(st);
        }
        [HttpPost]
        public IActionResult Index(String _Name,int _Marks)

        {
            _context.Add(new Student() { Name=_Name,Marks = _Marks });
            _context.SaveChanges();
            var st = _context.Students.ToList();
            return View(st);
        }
        [HttpPost]
        public IActionResult Delete(int Id)

        {
           var st = _context.Students.FirstOrDefault(x => x.Id == Id);
            if (st != null) { 
            _context.Students.Remove(st);
                _context.SaveChanges(); 
            }
            
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult UpdateStd(int Id)
        {
            var st = _context.Students.FirstOrDefault(x => x.Id == Id);
            if (st != null)
            {
                return View("EditStudent",st);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult UpdateStd(int Id,string Ename,int Emarks)
        {
            var st = _context.Students.Find(Id);
            if (st != null)
            {
                st.Name = Ename;
                st.Marks = Emarks;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
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
