namespace ExpenseApp.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }        
        public int ExpenseId { get; set; }
        public Guid UserId { get; set; }
        public string? AdditionalNote { get; set; }
        public int Amount { get; set; }
    }
}
