using ExpenseApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace ExpenseApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;

        public AccountController(HttpClient client)
        {
            _client = client;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _client.PostAsJsonAsync($"https://localhost:44379/api/Account/signIn/{model.UserName}/{model.Password}", new { });
                var user = JsonSerializer.Deserialize<UserDetails>(await response.Content.ReadAsStringAsync(),new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (user != null && !string.IsNullOrEmpty(user.FirstName))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("AuthToken", user.Token) // Custom claim for the authentication token
                        // Add more claims as needed
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        // Set properties if needed
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid UserName or Password");
                }
            }
            return View(model);
        }
    }
}
