using ExpenseTracker.Api.Data;
using ExpenseTracker.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.Api
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUp(SignUpModel model);
        Task<UserDetailsModel> SignIn(string UserName, string Password);
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountRepository(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
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

        public async Task<UserDetailsModel> SignIn(string username,string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    return new();
                }

                var role = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>() {
                   new Claim(ClaimTypes.Name,username),
                   new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
               };

                var authSignKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _config["JWT:ValidUser"],
                    audience: _config["JWT:ValidAudience"],
                    expires: DateTime.Now.AddDays(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256Signature));
                return new UserDetailsModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email ?? string.Empty,
                    Role = role.FirstOrDefault() ?? string.Empty,
                    UserName = user.UserName ?? string.Empty,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
            return new();
        }
    }
}
