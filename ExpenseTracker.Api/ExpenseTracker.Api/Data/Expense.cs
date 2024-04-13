using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Data
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ExpenseId { get; set; }
        public string UserId { get; set; }
        public string? AdditionalNote { get; set; }
        public int Amount { get; set; }
    }
}
