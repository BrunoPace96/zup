namespace ZupTeste.Repository.UnitOfWork.Factories
{
    public abstract class UnitOfWorkScopeFactoryBase : IUnitOfWorkScopeFactory
    {
        private IUnitOfWorkScope _scope;
        public bool ScopeOpened => _scope is { Committed: false };

        public IUnitOfWorkScope Get()
        {
            if (!ScopeOpened)
                _scope = CreateNew();

            return _scope;
        }
        protected abstract IUnitOfWorkScope CreateNew();
    }
}