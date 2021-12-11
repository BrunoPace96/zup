namespace ZupTeste.Domain.Funcionarios.Write.CriarFuncionario;

public record CriarFuncionarioResult
{
    public Guid Id { get; set; }
    
    public string Nome { get; set; }
    
    public string Sobrenome { get; set; }
    
    public string Email { get; set; }

    public int NumeroChapa { get; set; } 
    
    public Guid? LiderId { get; set; }
}