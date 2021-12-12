namespace ZupTeste.Core.Contracts
{
    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; }
        DateTime LastUpdatedAt { get; }
        
        void Created();
        void Updated();
    }
}