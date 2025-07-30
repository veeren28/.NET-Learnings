using System.ComponentModel.DataAnnotations;

namespace ExpensesTracker.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
    }

}
