using System.Linq.Expressions;
using Ardalis.Specification;
using ZupTeste.DataContracts.Queries;
using ZupTeste.DataContracts.Results;

namespace ZupTeste.Repository.Repository
{
    public interface IReadOnlyRepository<TEntity>
    {
        Task<TEntity> FirstOrDefaultAsync(Guid id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task<bool> CheckIfExistsAsync(Guid id);
        
        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> FirstOrDefaultAsync<TSpecification>(TSpecification specification) 
            where TSpecification: ISpecification<TEntity>;
        
        Task<IList<TEntity>> QueryAsync<TSpecification>(TSpecification specification) 
            where TSpecification: ISpecification<TEntity>;
        
        Task<PaginatedResult<TEntity>> QueryPagedAndCountAsync<TSpecification, TQuery>(
            TSpecification specification,
            FilterQuery<TQuery> query
        ) where TSpecification : ISpecification<TEntity>;
        
        Task<bool> CheckIfExistsAsync<TSpecification>(TSpecification specification) 
            where TSpecification: ISpecification<TEntity>;
        
        Task<TEntity> ReloadAsync(TEntity instance);
    }
}