namespace ExpensesTracker.DTOs.SummaryDTO_s
{
    public class ExpenseSummaryByMonthDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
