using System.ComponentModel.DataAnnotations;

namespace RoleBasedAuthentication.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="passwords dont match")]
public string ConfirmPassword { get; set; }


    }
}
