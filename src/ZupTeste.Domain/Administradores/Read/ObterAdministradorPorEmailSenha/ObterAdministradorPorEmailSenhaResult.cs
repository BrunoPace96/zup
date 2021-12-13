namespace ZupTeste.Domain.Administradores.Read.ObterAdministradorPorEmailSenha
{
    public record ObterAdministradorPorEmailSenhaResult
    {
        public Guid Id { get; set; }
        
        public string Nome { get; set; }
        
        public string Email { get; set; }
    }
}