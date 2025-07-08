using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameWorkPract.Models
{
    public class RegisterModel 
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords doesntMatch")]
        public string ConfirmPassword { get; set; }

    }
}
