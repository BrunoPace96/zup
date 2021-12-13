using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ZupTeste.API.Authentication.Contracts;
using ZupTeste.Infra.Settings;

namespace ZupTeste.API.Authentication
{
    public class JwtHelper
    {
        private readonly AppSettings _appSettings;

        public JwtHelper(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public RespostaToken GenerateToken(AdministradorAutenticado user)
        {
            var bearerToken = GenerateBearerToken(user);

            var result = new RespostaToken
            {
                AccessToken = bearerToken.Token,
                ExpiresIn = (int)TimeSpan.FromSeconds(_appSettings.JwtSettings.ExpiresIn).TotalSeconds,
                RespostaAdministrador = new RespostaAdministradorToken
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                }
            };

            return result;
        }

        public TokenAcesso GenerateBearerToken(AdministradorAutenticado user)
        {
            var identity = new ClaimsIdentity
            (
                new GenericIdentity(user.Email),
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.GivenName, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
                }
            );
            

            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSettings.Secret);
            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = _appSettings.JwtSettings.Audience,
                Issuer = _appSettings.JwtSettings.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = identity,
                Expires = DateTime.UtcNow.AddSeconds(_appSettings.JwtSettings.ExpiresIn)
            });

            return new TokenAcesso
            {
                Token = handler.WriteToken(securityToken),
                ClaimsIdentity = identity
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
