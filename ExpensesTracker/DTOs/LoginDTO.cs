using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTOs
{
    public class LoginDTO
    {
        [Required]
        
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }
}
