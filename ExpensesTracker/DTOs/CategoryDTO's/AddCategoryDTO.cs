using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTOs
{
    public class AddCategoryDTO
    {

        [Required (ErrorMessage ="Category should not be empty")]
        public string CategoryName { get; set; }

    }
}
