using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Data
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ExpenseId { get; set; }
        public Guid UserId { get; set; }
        public string? AdditionalNote { get; set; }
        public int Amount { get; set; }
    }
}
