using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using techPracticeApiDS.Models;

namespace techPracticeApiDS.Services
{
    public class AuthService
    {
        private readonly string _key;
        public static List<User> Users { get; } = new List<User>
        {
            new User { Id = 1, Name = "Alice Johnson", Email = "alice@example.com", Password = BCrypt.Net.BCrypt.HashPassword("password123") },
            new User { Id = 2, Name = "Bob Smith", Email = "bob@example.com", Password = BCrypt.Net.BCrypt.HashPassword("securepass") }
        };

        public AuthService(string key) => _key = key;

        public string? Login(string email, string password)
        {
            var user = Users.FirstOrDefault(u => u.Email == email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password)) return null;

            return GenerateJwtToken(user.Email);
        }

        private string GenerateJwtToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}