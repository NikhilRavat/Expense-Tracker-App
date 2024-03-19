using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Data
{
    public class ExpenseDbContext : IdentityDbContext<ApplicationUser>
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options):base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Enumerations> Enumerations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
