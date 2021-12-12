using Ardalis.Specification;

namespace ZupTeste.Domain.Funcionarios.Read.AtualizarFuncionario
{
    public sealed class AtualizarFuncionarioSpecificaition : Specification<Funcionario>
    {
        public AtualizarFuncionarioSpecificaition(Guid id)
        {
            Query
                .Include(x => x.Telefones)
                .Where(x => x.Id == id);
        }
    }
}