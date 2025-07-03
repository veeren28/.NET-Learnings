using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using StudentPractice.Models;

namespace StudentPractice.Controllers
{

          
    public class HomeController : Controller
    {
        public static List<Student> std = new List<Student>
        {
            new Student{Name ="Veeren", Rollno=005, Marks=100 },
            new Student{Name = "Sandeep",Rollno=002,Marks =100},
            new Student{Name = "Hanu",Rollno=001,Marks=90},
        };
      public  static HashSet<int> RollnoSet = new HashSet<int>();
        static HomeController()
        {
            foreach(Student student in std)
            {
                RollnoSet.Add(student.Rollno);
            }
        }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]        
        
        public IActionResult Index()

        {
            ViewBag.Message = null;
            

            return View(std);
        }
        [HttpPost]
        public IActionResult Index(string name , int rollno,int marks) {
            
            if (RollnoSet.Contains(rollno))
            {
                ViewBag.Message = "Roll Number Already  Exists,Student cannot be Added";
            }
            else
            {
                std.Add(new Student { Name = name, Rollno = rollno, Marks = marks });
                RollnoSet.Add(rollno);
            }
            return View(std);

        }
        
        [HttpPost]
        public IActionResult Delete(int Id)
        { int index = 1;
            
            for(int i = 0;i< std.Count; i++)
            {
                if (std[i].Rollno == Id)
                {
                    std.RemoveAt(i);
                    RollnoSet.Remove(Id);
                    break;
                }
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditStudent(int Id)
        { var s = std.FirstOrDefault(s=>s.Rollno == Id);
            if(s == null) { return NotFound(); }
            return View("EditStudent",s);

        }
        [HttpPost]
        public IActionResult EditStudent(int id,string name,int marks)
        {
           
            foreach (Student student in std)
            {
                if (student.Rollno == id)
                {
                    student.Name = name;    
                    student.Marks = marks;
                    
                }
                
            }
            return RedirectToAction("Index");
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
