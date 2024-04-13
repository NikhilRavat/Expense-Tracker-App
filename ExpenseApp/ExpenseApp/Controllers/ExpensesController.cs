using ExpenseApp.Helpers;
using ExpenseApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseApp.Controllers
{

    [CustomAuthorize]
    public class ExpensesController : Controller
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }
        public async Task<IActionResult> Index()
        {
            var expenses = await _expenseService.GetExpensesByUserIdAsync(User.FindFirst("UserId")?.Value ?? "");
            return View(expenses);
        }

        public ViewResult AddExpenses()
        {
            return View();
        }
    }
}
