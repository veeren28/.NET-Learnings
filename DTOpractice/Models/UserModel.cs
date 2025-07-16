using System.ComponentModel.DataAnnotations;

namespace DTOpractice.Models
{
    public class UserModel 
    {
        public int Id { get; set; }
        [Required] public string UserName { get; set; }
        [Required] public string Email { get; set; }

        [Required] [DataType(DataType.Password)]public string Password { get; set; }
        public DateTime dateCreated { get; set; }




    }
}
