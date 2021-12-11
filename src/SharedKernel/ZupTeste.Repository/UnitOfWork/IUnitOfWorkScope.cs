namespace ZupTeste.Repository.UnitOfWork
{
    public interface IUnitOfWorkScope
    {
        bool Committed { get; }
        void Rollback();
        Task CommitAsync();
    }
}