using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTOs
{
    public class AddCategoryDTO
    {

        [Required (ErrorMessage ="Category should not be empty")]
        [RegularExpression(@"^\S+$", ErrorMessage = "Category name cannot contain whitespace.")]
        public string CategoryName { get; set; }

    }
}
