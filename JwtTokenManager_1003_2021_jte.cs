// 代码生成时间: 2025-10-03 20:21:08
using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace JwtManagerApp
{
    public class JwtTokenManager
    {
        private readonly SymmetricSecurityKey key;
        private readonly string issuer;
        private readonly string audience;

        // Constructor
        public JwtTokenManager(string secretKey, string issuer, string audience)
        {
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentException("Secret key is required.", nameof(secretKey));

            this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            this.issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            this.audience = audience ?? throw new ArgumentNullException(nameof(audience));
        }

        // Generate a JWT token
        public string GenerateToken(string username)
        {
            try
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, username)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var now = DateTime.UtcNow;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = now.AddHours(1),
                    SigningCredentials = new SigningCredentials(this.key, SecurityAlgorithms.HmacSha256Signature),
                    Issuer = this.issuer,
                    Audience = this.audience
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                // Proper error handling can be added here
                throw new InvalidOperationException("Error generating JWT token.", ex);
            }
        }

        // Validate a JWT token
        public bool ValidateToken(string token, out string username)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = this.key,
                        ValidateIssuer = true,
                        ValidIssuer = this.issuer,
                        ValidateAudience = true,
                        ValidAudience = this.audience,
                        ValidateLifetime = true, // Check if token is expired
                        ClockSkew = TimeSpan.Zero // No clock skew
                    },
                    out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                username = jwtToken.Claims.First(c => c.Type == ClaimTypes.Name)?.Value;
                return true;
            }
            catch (Exception)
            {
                username = null;
                return false;
            }
        }
    }
}
