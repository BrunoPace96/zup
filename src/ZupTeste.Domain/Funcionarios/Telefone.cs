using ZupTeste.Core;

namespace ZupTeste.Domain.Funcionarios;

public class Telefone : EntityBase
{
    public string Numero { get; set; }
    
    
    // Relationships
    
    public Guid FuncionarioId { get; set; }
    
    public Funcionario Funcionario { get; set; }
}