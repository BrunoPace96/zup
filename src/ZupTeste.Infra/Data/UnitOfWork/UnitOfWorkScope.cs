using MediatR;
using Microsoft.EntityFrameworkCore;
using ZupTeste.DomainValidation.Domain;
using ZupTeste.Infra.Data.Context;
using ZupTeste.Infra.Data.Extensions;
using ZupTeste.Repository.UnitOfWork;

namespace ZupTeste.Infra.Data.UnitOfWork;

public class UnitOfWorkScope : IUnitOfWorkScope
{
    public bool Committed { get; private set; }
        
    private readonly DatabaseContext _context;
    private readonly IDomainValidationProvider _domainValidationProvider;
    private readonly IMediator _mediator;

    public UnitOfWorkScope(
        DatabaseContext context,
        IDomainValidationProvider domainValidationProvider,
        IMediator mediator
    )
    {
        _context = context;
        _domainValidationProvider = domainValidationProvider;
        _mediator = mediator;
    }

    public async Task CommitAsync()
    {
        if (_domainValidationProvider.HasNoErrors())
        {
            if (Committed)
                throw new Exception("UnitOfWork scope already committed.");
                    
            await _context.SaveChangesAsync();
            await _mediator.DispatchDomainEventsAsync(_context);
            Committed = true;
        }
    }

    public void Rollback() => 
        _context.ChangeTracker
            .Entries()
            .Where(e => e.Entity != null)
            .ToList()
            .ForEach(e =>
            {
                e.State = EntityState.Detached;
            });
}