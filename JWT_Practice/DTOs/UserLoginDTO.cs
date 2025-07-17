using System.ComponentModel.DataAnnotations;

namespace JWT_Practice.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string Password { get; set; }    

    }
}
