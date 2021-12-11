namespace ZupTeste.Domain.Funcionarios.Read.ObterListaFuncionarios;

public record ObterListaFuncionariosResult
{
    public Guid Id { get; set; }
    
    public string Nome { get; set; }
    
    public string Sobrenome { get; set; }
    
    public string Email { get; set; }

    public string NumeroChapa { get; set; }
    
    public List<string> Telefones { get; set; }
}