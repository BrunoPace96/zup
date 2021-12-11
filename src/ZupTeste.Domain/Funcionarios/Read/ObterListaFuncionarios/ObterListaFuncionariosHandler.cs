using AutoMapper;
using MediatR;
using ZupTeste.DataContracts.Queries;
using ZupTeste.DataContracts.Results;
using ZupTeste.DomainValidation.Domain;
using ZupTeste.Repository.Repository;

namespace ZupTeste.Domain.Funcionarios.Read.ObterListaFuncionarios
{
    public class ObterListaFuncionariosHandler : 
        IRequestHandler<PaginatedQuery<PaginatedResult<ObterListaFuncionariosResult>>, PaginatedResult<ObterListaFuncionariosResult>>
    {
        private readonly IReadOnlyRepository<Funcionario> _repository;
        private readonly IDomainValidationProvider _validator;
        private readonly IMapper _mapper;

        public ObterListaFuncionariosHandler(
            IReadOnlyRepository<Funcionario> repository,
            IDomainValidationProvider validator,
            IMapper mapper
        )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ObterListaFuncionariosResult>> Handle(
            PaginatedQuery<PaginatedResult<ObterListaFuncionariosResult>> request, 
            CancellationToken cancellationToken)
        {
            var specification = new ObterListaFuncionariosSpecification();
            
            var funcionarios = await _repository.QueryPagedAndCountAsync(
                specification,
                request);

            return _mapper.Map<PaginatedResult<ObterListaFuncionariosResult>>(funcionarios);
        }
    }
}