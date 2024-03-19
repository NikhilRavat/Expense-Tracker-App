using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Api
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUp(SignUpModel model);
        Task<SignInResult> SignIn(string UserName, string Password);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<IdentityResult> SignUp(SignUpModel model)
        {
            var result = await _userManager.CreateAsync(new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password
            }, model.Password);
            return result;
        }

        public async Task<SignInResult> SignIn(string UserName,string Password)
        {
            var result = await _signInManager.PasswordSignInAsync(UserName, Password, false, false);
            return result;
        }
    }
}
