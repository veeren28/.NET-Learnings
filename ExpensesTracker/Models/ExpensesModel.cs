using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.Models
{
    public class ExpensesModel
    {
        [Key]
        //this is the id for eachh expense
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserApplication User { get; set; } // Or rename to `User` or `Owner`

        [Required]
        [StringLength(100, ErrorMessage = "Title should be less than 100 characters")]
        public string Title { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "Amount Should be Greater Than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        
        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceAfter { get; set; }

        [Required]
        [StringLength(100)]
       
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Required]
        [StringLength(300)]
        public string? Description { get; set; }


        public DateTime? UpdatedAt { get; set; }


        // category
        [Required  ]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryModel Category { get; set; }
        // Navigation

        public int TransactionId { get; set; }
        public TransactionModel Transaction { get; set; }
    }
}
