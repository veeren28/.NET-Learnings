using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        
        public String  CategoryName { get; set; }
        [Required]
        
        public ICollection<ExpensesModel> Expenses { get; set; }


    }
}
