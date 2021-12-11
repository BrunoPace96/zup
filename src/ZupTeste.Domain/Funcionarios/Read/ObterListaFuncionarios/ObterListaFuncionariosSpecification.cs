using Ardalis.Specification;

namespace ZupTeste.Domain.Funcionarios.Read.ObterListaFuncionarios
{
    public sealed class ObterListaFuncionariosSpecification : Specification<Funcionario>
    {
        public ObterListaFuncionariosSpecification()
        {
            Query
                .Include(x => x.Telefones);
        }
    }
}