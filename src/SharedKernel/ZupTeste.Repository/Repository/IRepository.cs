using ZupTeste.Core.Contracts;

namespace ZupTeste.Repository.Repository
{
    public interface IRepository<TEntity> where TEntity: IAggregateRoot
    {
        Task SaveAsync(TEntity entity);
        void Delete(TEntity entity);
    }
}