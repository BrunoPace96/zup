using Microsoft.EntityFrameworkCore;
using ZupTeste.Core;
using ZupTeste.Core.Contracts;
using ZupTeste.DomainValidation.Domain;
using ZupTeste.Infra.Data.Context;
using ZupTeste.Repository.Repository;

namespace ZupTeste.Infra.Data.Repositories;

public sealed class Repository<TEntity> : IRepository<TEntity>
    where TEntity : EntityBase, IAggregateRoot
{
    private readonly DatabaseContext _context;
    private readonly IDomainValidationProvider _validator;
    private readonly DbSet<TEntity> _set;

    public Repository(
        DatabaseContext context,
        IDomainValidationProvider validator
    )
    {
        _context = context;
        _validator = validator;
        _set = _context.Set<TEntity>();
    }

    public async Task SaveAsync(TEntity entity)
    {
        if (_validator.HasErrors()) 
            return;
            
        if (_context.Entry(entity).State != EntityState.Modified)
        {
            entity.Created();
            await _set.AddAsync(entity);
        }
        else
        {
            entity.Updated();
            _context.Entry(entity).State = EntityState.Modified;
        }
    }

    public void Delete(TEntity entity)
    {
        _set.Remove(entity);
    }
}