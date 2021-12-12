using System.Linq.Expressions;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZupTeste.Core;
using ZupTeste.DataContracts.Queries;
using ZupTeste.DataContracts.Results;
using ZupTeste.Infra.Data.Context;
using ZupTeste.Repository.Repository;

namespace ZupTeste.Infra.Data.Repositories;

public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
    where TEntity : EntityBase
{
    private readonly DatabaseContext _context;
    private readonly DbSet<TEntity> _set;
    private readonly ISpecificationEvaluator _specificationEvaluator;

    public ReadOnlyRepository(DatabaseContext context)
    {
        _context = context;
        _set = _context.Set<TEntity>();
        _specificationEvaluator = SpecificationEvaluator.Default;
    }

    public virtual IQueryable<TEntity> GetQuery() =>
        _set.AsQueryable();

    public virtual async Task<TEntity> FirstOrDefaultAsync(Guid id) =>
        await _set.FirstOrDefaultAsync(e => e.Id == id);
        
    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) =>
        await _set.FirstOrDefaultAsync(predicate);
    
    public async Task<bool> CheckIfExistsAsync(Guid id) =>
        await _set.AsQueryable().AnyAsync(e => e.Id == id);
        
    public async Task<IList<TEntity>> GetAllAsync() =>
        await _set.AsQueryable().ToListAsync();

    public async Task<TEntity> FirstOrDefaultAsync<TSpecification>(TSpecification specification)
        where TSpecification : ISpecification<TEntity> =>
        await ApplySpecification(specification).FirstOrDefaultAsync();
        
    public async Task<PaginatedResult<TEntity>> QueryPagedAndCountAsync<TSpecification, TQuery>(
        TSpecification specification, 
        PaginatedQuery<TQuery> query
    ) where TSpecification : ISpecification<TEntity>
    {
        var limit = query.PageSize;
        var offset = (query.Page - 1) * query.PageSize;
        
        var items = await ApplySpecification(specification).Skip(offset).Take(limit).ToListAsync();
        var count = await ApplySpecification(specification).CountAsync();

        return new PaginatedResult<TEntity>
        {
            CurrentPage = query.Page,
            TotalPages = (int) Math.Ceiling(count / (decimal) query.PageSize),
            TotalItems = count,
            Items = items
        };
    }
        
    public async Task<IList<TEntity>> QueryAsync<TSpecification>(TSpecification specification)
        where TSpecification : ISpecification<TEntity> =>
        await ApplySpecification(specification).ToListAsync();
        
    public async Task<bool> CheckIfExistsAsync<TSpecification>(TSpecification specification)
        where TSpecification : ISpecification<TEntity> =>
        await ApplySpecification(specification).AnyAsync();
        
    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification) =>
        _specificationEvaluator.GetQuery(_set.AsQueryable(), specification);

    public async Task<TEntity> ReloadAsync(TEntity instance)
    {
        await _context.Entry(instance).ReloadAsync();
        return instance;
    }
}