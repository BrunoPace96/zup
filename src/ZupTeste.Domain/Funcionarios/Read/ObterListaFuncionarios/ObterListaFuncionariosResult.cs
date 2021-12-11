namespace ZupTeste.Domain.Funcionarios.Read.ObterListaFuncionarios;

public record ObterListaFuncionariosResult
{
    public string Nome { get; set; }
    
    public string Sobrenome { get; set; }
    
    public string Email { get; set; }

    public int NumeroChapa { get; set; }
    
    public List<string> Telefones { get; set; }
}