using ZupTeste.DomainValidation.DataContracts;
using ZupTeste.OperationResult.Implementations;

namespace ZupTeste.DomainValidation.Domain
{
    public interface IDomainValidationProvider
    {
        bool HasErrors();
        bool HasNoErrors();
        List<DomainValidationNotification> GetErrors();
        void AddValidationError(string message, string field = "default");
        void AddValidationError(DomainValidationNotification validationNotification);
        void AddValidationErrors(IEnumerable<DomainValidationNotification> validations);
        void AddUnauthorizedError(string message = "Unauthorized!");
        void AddForbiddenError(string message = "Forbidden!");
        void AddNotFoundError(string message = "Not Found!");
        void ValidateValueObjects(params ValueObjectResult<object>[] items);
    }
}