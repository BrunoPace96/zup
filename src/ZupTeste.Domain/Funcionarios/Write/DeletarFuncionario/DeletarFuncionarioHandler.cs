using AutoMapper;
using MediatR;
using ZupTeste.DataContracts.Commands;
using ZupTeste.DataContracts.Queries;
using ZupTeste.DomainValidation.Domain;
using ZupTeste.Repository.Repository;
using ZupTeste.Repository.UnitOfWork.Factories;

namespace ZupTeste.Domain.Funcionarios.Write.DeletarFuncionario
{
    public class DeletarFuncionarioHandler: 
        IRequestHandler<ByIdCommand<DeletarFuncionarioResult>, DeletarFuncionarioResult>
    {
        private readonly IUnitOfWorkScopeFactory _unitOfWork;
        private readonly IRepository<Funcionario> _repository;
        private readonly IReadOnlyRepository<Funcionario> _readOnlyRepository;
        private readonly IDomainValidationProvider _validator;
        private readonly IMapper _mapper;

        public DeletarFuncionarioHandler(
            IUnitOfWorkScopeFactory unitOfWork,
            IRepository<Funcionario> repository,
            IReadOnlyRepository<Funcionario> readOnlyRepository,
            IDomainValidationProvider validator,
            IMapper mapper
        )
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
            _validator = validator;
            _mapper = mapper;
        }
        
        public async Task<DeletarFuncionarioResult> Handle(
            ByIdCommand<DeletarFuncionarioResult> command, 
            CancellationToken cancellationToken
        )
        {
            var specification = new DeletarFuncionarioSpecification(command.Id);
            var funcionario = await _readOnlyRepository.FirstOrDefaultAsync(specification);

            if (funcionario == null)
            {
                _validator.AddNotFoundError();
                return null;
            }
            
            var scope = _unitOfWork.Get();
            _repository.Delete(funcionario);
            await scope.CommitAsync();
            
            return new DeletarFuncionarioResult
            {
                Sucesso = true
            };
        }
    }
}