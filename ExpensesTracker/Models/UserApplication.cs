using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.Models
{
    public class UserApplication : IdentityUser
    {
        //[Key]
        //public int UserId { get; set; }
        //[Required]
   
        //public string UserName { get; set; }
        //[Required]

        //public string Email { get; set; }
        // not required because they are already present in IdentityUser Class.
        [Required(ErrorMessage ="Balance Required")]
        [Precision(18, 2)]
        public decimal Balance { get; set; }  
        //[Required]

        ////[DataType(DataType.Password)]
        ////public string Password  { get; set; }




    }
}
