using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.Data
{
    public class Enumerations
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Table { get; set;}
        [Required]
        [MaxLength(100)]
        public string Column { get; set; }
        [Required]
        [MaxLength(100)]
        public string Text { get; set; }
        public int Value { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
