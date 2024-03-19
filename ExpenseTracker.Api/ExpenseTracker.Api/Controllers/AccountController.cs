using ExpenseTracker.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ExpenseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost("signUp")]
        public async Task<ActionResult<IdentityResult>> SignUp(SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.SignUp(model);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("signIn/{userName}/{password}")]
        public async Task<ActionResult<SignInResult>> SignIn(string userName,string password)
        {
            var result = await _accountRepository.SignIn(userName,password);
            return Ok(result);
        }
    }
}
