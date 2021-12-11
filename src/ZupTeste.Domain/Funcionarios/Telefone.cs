using ZupTeste.Core;
using ZupTeste.Core.Utils;

namespace ZupTeste.Domain.Funcionarios;

public class Telefone : EntityBase
{
    private string _numero;
    public string Numero { get => _numero; set => _numero = value.UnMask(); }
    
    
    // Relationships
    
    public Guid FuncionarioId { get; set; }
    
    public Funcionario Funcionario { get; set; }
}