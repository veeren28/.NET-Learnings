using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JWT_Practice
{
    public class ApplicationUser :IdentityUser
    {
        [Required]
        public string name { get;set;}
       
        
        [Required]
        public int phno { get; set;}

    }
}
