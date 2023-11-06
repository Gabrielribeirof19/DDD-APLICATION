using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using authentication.domain.entities;
using authentication.Domain.Commands;
using authentication.Domain.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Api.Extension
{
    public static class JwtExtension
    {
        public static string Generate(AuthenticateCommand data)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.SecretsConfiguration.JwtPrivateKey);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(data),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = credentials,
            };
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(AuthenticateCommand user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim("Id", user.RequestPersonId.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.name));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            foreach (var role in user.Roles)
                ci.AddClaim(new Claim(ClaimTypes.Role, role));

            return ci;
        }
    }
}