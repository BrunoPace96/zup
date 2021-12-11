using FluentValidation;
using MediatR;
using ZupTeste.DomainValidation.DataContracts;

namespace ZupTeste.DomainValidation.Domain.Behaviours
{
    public class FailFastBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IDomainValidationProvider _domainValidationProvider;

        public FailFastBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            IDomainValidationProvider domainValidationProvider
        )
        {
            _validators = validators;
            _domainValidationProvider = domainValidationProvider;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next
        )
        {
            var errors = _validators
                .Select(e => e.Validate(request))
                .SelectMany(e => e.Errors)
                .Select(e => new DomainValidationNotification(e.ErrorMessage, e.PropertyName))
                .ToList();

            if (errors.Any())
            {
                _domainValidationProvider.AddValidationErrors(errors);
                return default;
            }

            return await next();
        }
    }
}