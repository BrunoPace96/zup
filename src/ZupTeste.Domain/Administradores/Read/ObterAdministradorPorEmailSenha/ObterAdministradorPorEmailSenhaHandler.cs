using AutoMapper;
using MediatR;
using ZupTeste.Core.Utils;
using ZupTeste.DomainValidation.Domain;
using ZupTeste.Repository.Repository;

namespace ZupTeste.Domain.Administradores.Read.ObterAdministradorPorEmailSenha
{
    public class ObterAdministradorPorEmailSenhaHandler: 
        IRequestHandler<ObterAdministradorPorEmailSenhaQuery, ObterAdministradorPorEmailSenhaResult>
    {
        private readonly IReadOnlyRepository<Administrador> _repository;
        private readonly IDomainValidationProvider _validator;
        private readonly IMapper _mapper;

        public ObterAdministradorPorEmailSenhaHandler(
            IReadOnlyRepository<Administrador> repository,
            IDomainValidationProvider validator,
            IMapper mapper
        )
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }
        
        public async Task<ObterAdministradorPorEmailSenhaResult> Handle(
            ObterAdministradorPorEmailSenhaQuery query, 
            CancellationToken cancellationToken
        )
        {
            var specification = new ObterAdministradorPorEmailSenhaSpecification(query.Email);
            
            var administrador = await _repository.FirstOrDefaultAsync(specification);
            
            if (administrador == null)
            {
                _validator.AddNotFoundError();
                return null;
            }
            
            if (!PasswordUtil.Compare(administrador.Senha, query.Senha))
            {
                _validator.AddValidationError("Credenciais inválidas");
                return null;
            }

            return _mapper.Map<ObterAdministradorPorEmailSenhaResult>(administrador);
        }
    }
}