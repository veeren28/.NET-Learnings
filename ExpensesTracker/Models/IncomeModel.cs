using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesTracker.Models
{
    public class IncomeModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserApplication User { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title should be less than 100 characters")]
        public string Title { get; set; }

        [Required]
        [Range(0.01, 1000000, ErrorMessage = "Amount Should be Greater Than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

     

        public DateTime? UpdatedAt { get; set; }

        

        public int TransactionId { get; set; }  
        public TransactionModel Transaction { get; set; }
    }
} 