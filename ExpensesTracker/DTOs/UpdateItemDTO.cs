using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTOs
{
    public class UpdateItemDTO
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string? Title { get; set; }

        [Range(0.01, 1000000)]
        public decimal? Amount { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public string? CategoryName { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

}
