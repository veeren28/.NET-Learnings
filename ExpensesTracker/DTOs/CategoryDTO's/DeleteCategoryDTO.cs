using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTOs.CategoryDTO_s
{
    public class DeleteCategoryDTO
    {
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName;
    }
}
