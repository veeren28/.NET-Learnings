using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.DTOs
{
    public class AddIncomeDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title should be less than 100 characters")]
        public string Title { get; set; }

        [Required]
        [Range(0.01, 100000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public DateTime? Date { get; set; } // Optional, defaults to now if not provided
    }
} 