using AutoMapper;
using MediatR;
using ZupTeste.Core.Utils;
using ZupTeste.DomainValidation.Domain;
using ZupTeste.Repository.Repository;
using ZupTeste.Repository.UnitOfWork.Factories;

namespace ZupTeste.Domain.Funcionarios.Write;

public class CriarFuncionarioValidator : IRequestHandler<CriarFuncionarioCommand, CriarFuncionarioResult>
{
    private readonly IDomainValidationProvider _validator;
    private readonly IUnitOfWorkScopeFactory _unitOfWork;
    private readonly IRepository<Funcionario> _repository;
    private readonly IReadOnlyRepository<Funcionario> _readOnlyRepository;
    private readonly IMapper _mapper;

    public CriarFuncionarioValidator(
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

    public async Task<CriarFuncionarioResult> Handle(
        CriarFuncionarioCommand command,
        CancellationToken cancellationToken)
    {
        var funcionario = _mapper.Map<Funcionario>(command);
        
        if (!string.IsNullOrEmpty(command.LiderEmail))
        {
            var lider = await _readOnlyRepository.FirstOrDefaultAsync(x =>
                string.Equals(x.Email, command.LiderEmail, StringComparison.CurrentCultureIgnoreCase));

            if (lider is null)
            {
                _validator.AddValidationError(
                    $"Lider com o email {command.LiderEmail} não encontrado", nameof(command.LiderEmail));
                return null;
            }
        }

        funcionario.Senha = PasswordUtil.EncryptNewPassword(funcionario.Senha);
        
        var scope = _unitOfWork.Get();
        await _repository.SaveAsync(funcionario);
        await scope.CommitAsync();

        return _mapper.Map<CriarFuncionarioResult>(funcionario);
    }
}