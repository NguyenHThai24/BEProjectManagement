using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjectManagement.services;
using ProjectManagement.Models;
using Microsoft.AspNet.Identity;

namespace ProjectManagement.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserServices _authService;

        public AuthController(UserServices authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Get_User_Register get)
        {
            try
            {
                var result = _authService.RegisterUser(get);
                return Ok(new
                {
                    message = "User registered successfully",
                    data = result,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Get_User_Login get)
        {
            try
            {
                var result = _authService.LoginUser(get);

                // Lưu token vào cookies
                Response.Cookies.Append("AuthToken", result.Token, new CookieOptions
                {
                    HttpOnly = true, // Ngăn chặn JavaScript truy cập, giúp chống XSS
                    Secure = true,   // Chỉ gửi cookie qua HTTPS
                    SameSite = SameSiteMode.Strict, // Chống CSRF
                    Expires = DateTime.UtcNow.AddMinutes(60) // Token hết hạn sau 60 phút
                });

                return Ok(new
                {
                    message = "Login successful",
                    data = new
                    {
                        id = result.User.Id,
                        fullname = result.User.FullName,
                        email = result.User.Email,
                        phone = result.User.Phone
                    }

                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }



        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            return Ok("This is a protected API endpoint.");
        }
    }
}
