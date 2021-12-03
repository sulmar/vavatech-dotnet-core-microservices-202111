using AuthService.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure
{
    public class JwtTokenOptions
    {
        public string SecretKey { get; set; }
    }

    // dotnet add package System.IdentityModel.Tokens.Jwt
    public class JwtTokenService : ITokenService
    {
        private readonly JwtTokenOptions options;

        public JwtTokenService(IOptions<JwtTokenOptions> options)
        {
            this.options = options.Value;
        }

        public string CreateToken(User user)
        {
            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
            var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Login));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credentials,
                IssuedAt = DateTime.UtcNow,
                Issuer = "Vavatech"
            };

            var securityToken = tokenHandler.CreateToken(descriptor);

            string token = tokenHandler.WriteToken(securityToken);

            return token;


        }
    }
}
