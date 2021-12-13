using System.Security.Claims;

namespace ZupTeste.API.Authentication.Contracts
{
    public class TokenAcesso
    {
        public string Token { get; set; }
        
        public ClaimsIdentity ClaimsIdentity { get; set; }
    }
}
