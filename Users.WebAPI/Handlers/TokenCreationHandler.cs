using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Users.Core;
using Users.Core.Common;

namespace Users.WebApi.Handlers
{
    public class TokenCreationHandler
    {
        private readonly IConfiguration configuration;

        public TokenCreationHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenInstance = new Token();
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, "Subject"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim("DisplayName", user.FullName),
                        new Claim("Email", user.Email)
                    };

            tokenInstance.Expiration = DateTime.Now.AddMinutes(10);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("87870A2D-6009-4BF1-BA48-FF99DD7FDD89"));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                "http://example.com",
                "http://example.com",
                claims,
                expires: tokenInstance.Expiration,
                signingCredentials: signIn);

            tokenInstance.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

            CreateRefreshToken(tokenInstance);

            return tokenInstance;
        }

        private void CreateRefreshToken(Token tokenInstance)
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                tokenInstance.RefreshToken = Convert.ToBase64String(number);
            }
        }
    }
}