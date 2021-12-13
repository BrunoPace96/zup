namespace ZupTeste.API.Authentication.Contracts
{
    public class RespostaToken
    {
        public string AccessToken { get; set; }
        
        public int ExpiresIn { get; set; }

        public RespostaAdministradorToken RespostaAdministrador { get; set; }
    }

    public class RespostaAdministradorToken
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
