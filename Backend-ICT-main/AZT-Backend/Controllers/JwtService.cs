using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AZT_Backend.Controllers
{
    public class JwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expireMinutes;

        public JwtService(IConfiguration config)
        {
            _secretKey = config["Jwt:SecretKey"]!;
            _issuer = config["Jwt:Issuer"]!;
            _audience = config["Jwt:Audience"]!;
            _expireMinutes = int.Parse(config["Jwt:ExpireMinutes"] ?? "480");
        }

        // ── Tạo token từ User ────────────────────────────────
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name,           user.Username),
                new Claim(ClaimTypes.Role,           user.Role),
                new Claim("status",                  user.Status)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ── Đọc thông tin từ token (dùng trong BaseController) ─
        public ClaimsPrincipal? ValidateToken(string token)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var handler = new JwtSecurityTokenHandler();

                var principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return principal;
            }
            catch
            {
                return null; // Token không hợp lệ hoặc hết hạn
            }
        }
    }
}
