using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseApp.Helpers
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizeFilter))
        {
        }
    }

    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthorizeFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authToken = _httpContextAccessor.HttpContext.User.FindFirst("AuthToken")?.Value;

            if (string.IsNullOrEmpty(authToken))
            {
                // User is not authenticated, redirect to the login page
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        }
    }
}
