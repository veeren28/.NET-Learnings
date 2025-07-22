using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTOs
{
    public class UpdateItemDTO
    {
        public int Id { get; set; } // Used for lookup, not editable

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [Range(0.01, 1000000)]
        public decimal Amount { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; } // Expense date

        // Optional: Add UpdatedAt if you want to track it from frontend
        public DateTime? UpdatedAt { get; set; }
    }
}
