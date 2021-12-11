using System.Net;
using ZupTeste.DomainValidation.DataContracts;
using ZupTeste.OperationResult.Implementations;

namespace ZupTeste.DomainValidation.Domain
{
    public class DomainValidationProvider : IDomainValidationProvider
    {
        private readonly List<DomainValidationNotification> _validations = new();

        public bool HasErrors() => 
            _validations.Any();

        public bool HasNoErrors() => 
            !HasErrors();

        public List<DomainValidationNotification> GetErrors() => 
            _validations;

        public void AddValidationError(string message, string field = "default") =>
            AddValidationError(new DomainValidationNotification(message, field));
        public void AddValidationError(DomainValidationNotification validationNotification) => 
            _validations.Add(validationNotification);

        public void AddValidationErrors(IEnumerable<DomainValidationNotification> validations) =>
            _validations.AddRange(validations);
        
        public void AddUnauthorizedError(string message = "Unauthorized!") => 
            AddValidationError(new DomainValidationNotification(HttpStatusCode.Unauthorized, message));
        
        public void AddForbiddenError(string message = "Forbidden!") => 
            AddValidationError(new DomainValidationNotification(HttpStatusCode.Forbidden, message));
        
        public void AddNotFoundError(string message = "Not Found!") => 
            AddValidationError(new DomainValidationNotification(HttpStatusCode.NotFound, message));
        
        public void ValidateValueObjects(params ValueObjectResult<object>[] items)
        {
            var errors = items.Where(e => e.Failure)
                .SelectMany(e => e.Errors.Select(error => new DomainValidationNotification(error, nameof(e))))
                .ToList();
            
            if (errors.Any())
                AddValidationErrors(errors);
        }
    }
}