using MediatR;
using ZupTeste.DomainValidation.Domain;
using ZupTeste.Infra.Data.Context;
using ZupTeste.Repository.UnitOfWork;
using ZupTeste.Repository.UnitOfWork.Factories;

namespace ZupTeste.Infra.Data.UnitOfWork;

public class UnitOfWorkScopeFactory : UnitOfWorkScopeFactoryBase
{
    private readonly DatabaseContext _context;
    private readonly IDomainValidationProvider _domainValidationProvider;
    private readonly IMediator _mediator;

    public UnitOfWorkScopeFactory(
        DatabaseContext context,
        IDomainValidationProvider domainValidationProvider,
        IMediator mediator
    )
    {
        _context = context;
        _domainValidationProvider = domainValidationProvider;
        _mediator = mediator;
    }

    protected override IUnitOfWorkScope CreateNew() => 
        new UnitOfWorkScope(_context, _domainValidationProvider, _mediator);
}