//***CODE BEHIND - BY RODOLFO.FONSECA***//

using CodeBehind.PadraoProjeto.SegurancaJWT.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeBehind.PadraoProjeto.SegurancaJWT
{
    public static class TokenService
    {
        public static string _chave = "ece08956be4d457aa5f99e6dc023b89a";
        public static Token GenerateToken(Usuario user)
        {
            var obj = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Email, user.Email)
                });

            var expiracao = DateTime.UtcNow.AddHours(2);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_chave);

            var tokenDescriptor = new SecurityTokenDescriptor
            {                
                Subject = obj,
                Expires = expiracao,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var Segurancatoken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(Segurancatoken);

            return new()
            {
                Authenticated = true,
                Created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }
    }
}
