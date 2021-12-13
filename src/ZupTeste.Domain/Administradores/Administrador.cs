using ZupTeste.Core;
using ZupTeste.Core.Contracts;

namespace ZupTeste.Domain.Administradores;

public class Administrador : EntityBase, IAggregateRoot
{
    public string Nome { get; set; }
    
    public string Email { get; set; }
    
    public string Senha { get; set; }
}