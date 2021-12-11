using AutoMapper;
using MediatR;
using ZupTeste.DataContracts.Queries;
using ZupTeste.DomainValidation.Domain;
using ZupTeste.Repository.Repository;

namespace ZupTeste.Domain.Funcionarios.Read.ObterFuncionarioPeloId
{
    public class ObterFuncionarioPeloIdHandler: 
        IRequestHandler<ByIdQuery<ObterFuncionarioPeloIdResult>, ObterFuncionarioPeloIdResult>
    {
        private readonly IReadOnlyRepository<Funcionario> _repository;
        private readonly IDomainValidationProvider _validator;
        private readonly IMapper _mapper;

        public ObterFuncionarioPeloIdHandler(
            IReadOnlyRepository<Funcionario> repository,
            IDomainValidationProvider validator,
            IMapper mapper
        )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        
        public async Task<ObterFuncionarioPeloIdResult> Handle(
            ByIdQuery<ObterFuncionarioPeloIdResult> query, 
            CancellationToken cancellationToken
        )
        {
            var specification = new ObterFuncionarioPeloIdSpecification(query.Id);
            var student = await _repository.FirstOrDefaultAsync(specification);

            if (student == null)
            {
                _validator.AddNotFoundError();
                return null;
            }

            return _mapper.Map<ObterFuncionarioPeloIdResult>(student);
        }
    }
}