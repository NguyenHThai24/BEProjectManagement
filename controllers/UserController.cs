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
                var data = _authService.RegisterUser(get);
                return Ok(new ApiResponse
                (201, "User registered successfully",
                     data)
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiResponse(709, ex.Message)); // lỗi email đã tồn tại
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, ex.Message)); // lỗi server
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Get_User_Login get)
        {
            try
            {
                var data = _authService.LoginUser(get);

                // Lưu token vào cookies
                Response.Cookies.Append("AuthToken", data.Token, new CookieOptions
                {
                    HttpOnly = true, // Ngăn chặn JavaScript truy cập, giúp chống XSS
                    Secure = true,   // Chỉ gửi cookie qua HTTPS
                    SameSite = SameSiteMode.Strict, // Chống CSRF
                    Expires = DateTime.UtcNow.AddMinutes(60) // Token hết hạn sau 60 phút
                });

                return Ok(new ApiResponse(200, "Login successful", data));
                // {
                //     message = "Login successful",
                //     data = new
                //     {
                //         id = result.User.Id,
                //         fullname = result.User.FullName,
                //         email = result.User.Email,
                //         phone = result.User.Phone
                //     }

                // });
            }
            catch (Exception ex)
            {
                return Unauthorized(new ApiResponse(401, ex.Message));
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
