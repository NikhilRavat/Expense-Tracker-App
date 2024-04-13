using ExpenseApp.Models;

namespace ExpenseApp.Services
{
    public interface IExpenseService
    {
        Task<List<ExpenseModel>> GetExpensesByUserIdAsync(string UserId);
    }

    public class ExpenseService : IExpenseService
    {
        private readonly IHttpService _http;

        public ExpenseService(IHttpService http)
        {
            _http = http;
        }
        public async Task<List<ExpenseModel>> GetExpensesByUserIdAsync(string UserId)
        {
            return await _http.HttpGet<List<ExpenseModel>>($"Expense/{UserId}") ?? new();
        }
    }
}
