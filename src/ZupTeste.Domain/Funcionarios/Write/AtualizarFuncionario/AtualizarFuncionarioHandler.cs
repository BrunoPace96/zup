using AutoMapper;
using MediatR;
using ZupTeste.Core.Utils;
using ZupTeste.Domain.Funcionarios.Read.AtualizarFuncionario;
using ZupTeste.DomainValidation.Domain;
using ZupTeste.Repository.Repository;
using ZupTeste.Repository.UnitOfWork.Factories;

namespace ZupTeste.Domain.Funcionarios.Write.AtualizarFuncionario;

public class AtualizarFuncionarioHandler : IRequestHandler<AtualizarFuncionarioCommand, AtualizarFuncionarioResult>
{
    private readonly IDomainValidationProvider _validator;
    private readonly IUnitOfWorkScopeFactory _unitOfWork;
    private readonly IRepository<Funcionario> _repository;
    private readonly IReadOnlyRepository<Funcionario> _readOnlyRepository;
    private readonly IMapper _mapper;

    public AtualizarFuncionarioHandler(
        IDomainValidationProvider validator,
        IUnitOfWorkScopeFactory unitOfWork,
        IRepository<Funcionario> repository,
        IMapper mapper, 
        IReadOnlyRepository<Funcionario> readOnlyRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _readOnlyRepository = readOnlyRepository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<AtualizarFuncionarioResult> Handle(
        AtualizarFuncionarioCommand command,
        CancellationToken cancellationToken)
    {
        var funcionario = await _readOnlyRepository
            .FirstOrDefaultAsync(new AtualizarFuncionarioSpecificaition(command.Id));
        
        if (funcionario == null)
        {
            _validator.AddNotFoundError();
            return null;
        }
        
        _mapper.Map(command, funcionario);
        
        if (!string.IsNullOrEmpty(command.LiderEmail))
        {
            var lider = await _readOnlyRepository.FirstOrDefaultAsync(x =>
                string.Equals(x.Email, command.LiderEmail, StringComparison.CurrentCultureIgnoreCase));

            if (lider is null)
            {
                _validator.AddValidationError(
                    $"Líder com o email {command.LiderEmail} não encontrado", nameof(command.LiderEmail));
                return null;
            }

            funcionario.LiderId = lider.Id;
        }
        
        var scope = _unitOfWork.Get();
        await _repository.SaveAsync(funcionario);
        await scope.CommitAsync();

        return _mapper.Map<AtualizarFuncionarioResult>(funcionario);
    }
}