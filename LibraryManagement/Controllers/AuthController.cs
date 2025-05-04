using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // Dummy user validation (replace with real logic later)
            if (loginRequest.Username == "admin" && loginRequest.Password == "password")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, loginRequest.Username),
                    new Claim(ClaimTypes.Role, "Admin") // Optional: for role-based auth later
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super_secret_key_1234567890_ABCDEFGH"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "yourapi",
                    audience: "yourclient",
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
