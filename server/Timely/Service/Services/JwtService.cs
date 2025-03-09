using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class JwtService
    {
        private readonly string _secretKey;

        public JwtService(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Key"]; // קבלת המפתח מתוך הקובץ
        }

        // פונקציה ליצירת טוקן עם תוקף של שעה
        public string GenerateToken(string userName, string userRole, string userEmail)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (string.IsNullOrEmpty(_secretKey))
            {
                throw new ArgumentNullException(nameof(_secretKey), "Secret key is empty.");
            }

            // המרת המפתח למערך בייטים (bytes) בהנחה שהמפתח לא ריק
            var key = Encoding.UTF8.GetBytes(_secretKey);

            // יצירת תיאור הטוקן
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Role, userRole),
            new Claim(ClaimTypes.Email, userEmail)
            }),
                Expires = DateTime.UtcNow.AddHours(1),

                Audience = "TimelyAudience",  
                Issuer = "Timely",

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // פונקציה לבדיקה אם טוקן תקף
        public (string UserName, string Role, string Email) ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_secretKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                // שליפת הנתונים מהטוקן
                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                var role = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
                var email = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

                return (userId, role, email);
            }
            catch
            {
                return (null, null, null);
            }
        }
    }
}
