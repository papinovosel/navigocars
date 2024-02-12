using Api.Filters.AuthorizationFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Api.Controllers
{
    [Route("api/admin/")]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [TypeFilter(typeof(AdminAuthorizationFilter))]
        public IActionResult Get()
        {
            return Ok("yepii");
        }

        [HttpPost("login")]
        public IActionResult Login(string password)
        {
            password = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            if (password == _configuration["AdminPassword"])
            {
                HttpContext.Response.Cookies.Append("Authorization", password, new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.Now.AddMinutes(15)
                });

                return Ok("Successfully logged in");
            }

            return Problem("Password invalid", statusCode: 500, title: "Login");
        }
    }
}
