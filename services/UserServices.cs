using ProjectManagement.Data;
using ProjectManagement.Models;
using System.Linq;
using BCrypt.Net;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectManagement.services
{
    public class UserServices
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserServices(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtSettings["ExpiresInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Đăng ký
        public User RegisterUser(Get_User_Register get)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == get.Email))
                {
                    throw new ArgumentException("Email already exists.");
                }

                string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$";
                if (!Regex.IsMatch(get.PasswordHash, passwordPattern))
                {
                    throw new ArgumentException("Invalid password format. Password must be 8-16 characters and include at least one uppercase letter, one lowercase letter, one number, and one special character.");
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(get.PasswordHash);

                User newUser = new User
                {
                    FullName = get.FullName,
                    PasswordHash = hashedPassword,
                    Phone = string.IsNullOrWhiteSpace(get.Phone) ? "" : get.Phone,
                    Email = get.Email
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return newUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in RegisterUser: {ex.Message}");
            }
        }

        // Đăng nhập
        //public string LoginUser(Get_User_Login get)
        //{
        //    var existingUser = _context.Users.SingleOrDefault(u => u.Email == get.Email);
        //    if (existingUser == null || !BCrypt.Net.BCrypt.Verify(get.Password, existingUser.Password))
        //    {
        //        throw new Exception("Invalid email or password.");
        //    }

        //    return GenerateJwtToken(existingUser);
        //}
        public (string Token, User User) LoginUser(Get_User_Login get)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Email == get.Email);

            // Kiểm tra user tồn tại và mật khẩu đúng
            if (existingUser == null || string.IsNullOrEmpty(get.PasswordHash) || !BCrypt.Net.BCrypt.Verify(get.PasswordHash, existingUser.PasswordHash))
            {
                throw new Exception("Invalid email or password.");
            }

            string token = GenerateJwtToken(existingUser);
            return (token, existingUser);
        }
    }
}
