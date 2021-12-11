using Ardalis.Specification;

namespace ZupTeste.Domain.Funcionarios.Read.ObterFuncionarioPeloId
{
    public sealed class ObterFuncionarioPeloIdSpecification : Specification<Funcionario>
    {
        public ObterFuncionarioPeloIdSpecification(Guid id)
        {
            Query
                .Include(x => x.Lider)
                    .ThenInclude(x => x.Telefones)
                .Include(x => x.Telefones)
                .Where(x => x.Id == id);
        }
    }
}