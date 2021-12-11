using MediatR;
using ZupTeste.Core.Utils;

namespace ZupTeste.Domain.Funcionarios.Write.CriarFuncionario;

public record CriarFuncionarioCommand : IRequest<CriarFuncionarioResult>
{
    public string Nome { get; set; }
        
    public string Sobrenome { get; set; }
        
    public string Email { get; set; }
        
    public string NumeroChapa { get; set; }

    public string Senha { get; set; }
        
    public string LiderEmail { get; set; }

    private IEnumerable<string> _telefones;
    public IEnumerable<string> Telefones
    {
        get => _telefones;
        set => _telefones = value.Select(x => x.UnMask());
    }
}