using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtTest.Jwt
{
    public class JwtHelper
    {
        public static string GenerateJwt (JwtDto JwtInfo)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecretKey));

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {

                new Claim("id",JwtInfo.Id.ToString()),
                new Claim("email",JwtInfo.Email),
                new Claim("usertype",JwtInfo.UserType.ToString()),

                new Claim(ClaimTypes.Role,JwtInfo.UserType.ToString()),

            };
            var expireMinutes = DateTime.Now.AddMinutes(JwtInfo.ExpireMinutes);

            var tokenDescriptor = new JwtSecurityToken(JwtInfo.Issuer, JwtInfo.Audience, claims, null, expireMinutes, credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return token;




        }

    }
}
