namespace ZupTeste.Domain.Funcionarios.Read.ObterFuncionarioPeloId
{
    public record ObterFuncionarioPeloIdResult
    {
        public Guid Id { get; set; }
        
        public string Nome { get; set; }
    
        public string Sobrenome { get; set; }
    
        public string Email { get; set; }

        public string NumeroChapa { get; set; }
    
        public List<string> Telefones { get; set; }
        
        public ObterFuncionarioPeloIdResult Lider { get; set; }
    }
}