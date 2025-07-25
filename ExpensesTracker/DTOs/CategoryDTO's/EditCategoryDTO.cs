using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTOs
{
    public class EditCategoryDTO
    {[Required(ErrorMessage ="Category Name is Required")]
    public string CategoryName { get; set; }
    
    }
}
