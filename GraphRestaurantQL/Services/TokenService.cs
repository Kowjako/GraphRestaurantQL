using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace GraphRestaurantQL.Services
{
    public interface ITokenService
    {
        string GetToken();
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _cfg;

        public TokenService(IConfiguration cfg)
        {
            _cfg = cfg;
        }

        public string GetToken()
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, "graphiQl"),
                new Claim(JwtRegisteredClaimNames.UniqueName, "graphiQl")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["TokenKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescr = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = "test"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescr);

            return tokenHandler.WriteToken(token);
        }
    }
}
