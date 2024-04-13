using ExpenseTracker.Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseDbContext _context;

        public ExpenseController(ExpenseDbContext context)
        {
            _context = context;
        }
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetExpensesForUser(string UserId)
        {
            return Ok(await _context.Expenses.Where(x => x.UserId == UserId).ToListAsync());
        }
    }
}
