using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NcnApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using NcnApi.Data;
using System.Text;
using System.Linq;

namespace NcnApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthCntroller : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string _key = "ThisIsASecurityJwtKeyCode@2025";

        public AuthCntroller(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);

            if (user == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}
