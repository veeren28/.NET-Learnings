using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace EntityFwDay1.Models
{
    public class Student
    {
        [Key]
        public int Rollno { get; set; }
        public string Name { get; set; }
    }
}
