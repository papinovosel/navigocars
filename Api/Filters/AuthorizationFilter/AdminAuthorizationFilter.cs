using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Api.Filters.AuthorizationFilter
{
    public class AdminAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public AdminAuthorizationFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authCookie = context.HttpContext.Request.Cookies["Authorization"];
            var expectedToken = _configuration["AdminPassword"];

            if (authCookie != expectedToken)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
