using Ardalis.Specification;

namespace ZupTeste.Domain.Funcionarios.Write.DeletarFuncionario
{
    public sealed class DeletarFuncionarioSpecification : Specification<Funcionario>
    {
        public DeletarFuncionarioSpecification(Guid id)
        {
            Query
                .Include(x => x.Telefones)
                .Where(x => x.Id == id);
        }
    }
}