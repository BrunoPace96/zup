using ZupTeste.Core;
using ZupTeste.Core.Contracts;

namespace ZupTeste.Domain.Funcionarios;

public class Funcionario : EntityBase, IAggregateRoot
{
    public string Nome { get; set; }
    
    public string Sobrenome { get; set; }
    
    public string Email { get; set; }

    public string NumeroChapa { get; set; } 

    public string Senha { get; set; }

    
    // Relationships
    
    public Guid? LiderId { get; set; }
    
    public Funcionario Lider { get; set; }
    
    public List<Funcionario> Funcionarios { get; set; }
    
    public List<Telefone> Telefones { get; set; }
}