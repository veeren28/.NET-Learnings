using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTOs
{
    public class EditCategoryDTO
    {
        
        [Required(ErrorMessage ="Category Name is Required")]
        [RegularExpression(@"^\S+$", ErrorMessage = "Category name cannot contain whitespace.")]
        public string CategoryName { get; set; }
    
    }
}
