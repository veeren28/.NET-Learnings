using System.ComponentModel.DataAnnotations;

namespace EntityFrameWorkPract.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
       public string Name { get; set; }
        [Required]
        public int Marks { get; set; }
    
      
    }
}
