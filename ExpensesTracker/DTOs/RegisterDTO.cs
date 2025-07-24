using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTOs
{
    public class RegisterDTO
    {
        // Validates required and email format
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        // Validates presence and minimum length
        [Required(ErrorMessage = "Username is required")]
        [MinLength(8, ErrorMessage = "Username must be at least 8 characters long")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Enter Your Balance")]public decimal ? Balance { get; set; }
        // w/o ? by default Balance value is assigned as null and the errror Is not
        //(cont) triggered when user register w/o filling the Balance Section

        // Validates password with minimum length
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Confirm password must match password
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
