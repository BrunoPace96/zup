using Ardalis.Specification;

namespace ZupTeste.Domain.Administradores.Read.ObterAdministradorPorEmailSenha
{
    public sealed class ObterAdministradorPorEmailSenhaSpecification : Specification<Administrador>
    {
        public ObterAdministradorPorEmailSenhaSpecification(string email)
        {
            Query
                .Where(x => x.Email.ToLower() == email.ToLower());
        }
    }
}