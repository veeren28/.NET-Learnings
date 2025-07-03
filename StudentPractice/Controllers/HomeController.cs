using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]        
        
        public IActionResult Index()

        {     


            return View(std);
        }
        [HttpPost]
        public IActionResult Index(string name , int rollno,int marks) { 
            std.Add(new Student{Name = name, Rollno=rollno, Marks=marks});
            return View(std);

        }
        
        [HttpPost]
        public IActionResult Delete(int Id)
        { int index = 1;
            foreach(Student student in std)
            {
                if (student.Rollno == Id) {
                    std.RemoveAt(
                    index);
                }
                index++;
            }
            return View(std);
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
            return View(std);
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
