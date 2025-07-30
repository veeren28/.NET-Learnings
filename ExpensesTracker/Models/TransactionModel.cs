using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.Models
{
    public class TransactionModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        public string Type { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public string UserID { get; set; }

        [ForeignKey("UserID")]
        public UserApplication Username { get; set; }

        public string CategoryName { get; set; }

        public int CategoryId { get; set; }

        // These will establish one-to-one relationships
        public IncomeModel? Income { get; set; }
        public ExpensesModel? Expenses { get; set; }
    }
}
