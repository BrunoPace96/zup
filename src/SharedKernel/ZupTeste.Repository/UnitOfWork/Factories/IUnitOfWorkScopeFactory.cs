namespace ZupTeste.Repository.UnitOfWork.Factories
{
    public interface IUnitOfWorkScopeFactory
    {
        bool ScopeOpened { get; }
        IUnitOfWorkScope Get();
    }
}