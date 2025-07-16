using System.ComponentModel.DataAnnotations;

namespace DTOpractice.Models
{
    public class CreateUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }
}
