using System.ComponentModel.DataAnnotations;

namespace JWT_Practice.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int phno { get; set; }
    }
}
