using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ZupTeste.DataContracts.Results;
using ZupTeste.DomainValidation.Domain;

namespace ZupTeste.API.Common.Filters
{
    public class DomainValidationFilter : IActionFilter
    {
        private readonly IDomainValidationProvider _validator;
        private readonly IWebHostEnvironment _env;

        public DomainValidationFilter(
            IDomainValidationProvider validator,
            IWebHostEnvironment env
            )
        {
            _validator = validator;
            _env = env;
        }
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is DbUpdateException exception)
                {
                    CreateResponse(context, HttpStatusCode.BadRequest,
                        GetErrors(exception));
                }
                else if (!_env.IsDevelopment())
                {
                    CreateResponse(context, HttpStatusCode.InternalServerError, GetErrors(context.Exception));
                }
            }
            else
            {            
                if (_validator.HasNoErrors())
                    return;
                
                if (CheckForPrioritizedFails(context))
                    return;

                CheckForBadRequestFails(context);
            }
        }

        private ValidationFailedResult GetErrors(Exception ex)
        {
            var error = new ValidationErrorResult
            {
                Field = "General",
                Messages = new List<string> {ex.Message}
            };

            var innerException = ex.InnerException;
            while (innerException != null)
            {
                error.Messages.Add(innerException.Message);
                innerException = innerException.InnerException;
            }

            return new ValidationFailedResult("General", new List<ValidationErrorResult> {error});
        }
        
        private void CreateResponse(ActionExecutedContext context, HttpStatusCode statusCode, ValidationFailedResult errors)
        {
            var result = new BadRequestObjectResult(errors)
            {
                StatusCode = (int)statusCode
            };

            result.ContentTypes.Add(MediaTypeNames.Application.Json);

            context.Result = result;
            context.ExceptionHandled = true;
        }
        
        private void CheckForBadRequestFails(ActionExecutedContext context)
        {
            var errors = _validator
                .GetErrors()
                .GroupBy(x => x.Field)
                .Select(e => new ValidationErrorResult
                {
                    Field = e.Key,
                    Messages = e.Select(m => m.Message).ToList()
                })
                .ToList();

            if (errors.Count == 0)
                return;

            var validationFailedResult = new ValidationFailedResult("Erro ao executar operação", errors);
            context.Result = new BadRequestObjectResult(validationFailedResult);
        }
        
        private bool CheckForPrioritizedFails(ActionExecutedContext context)
        {
            var statusCodes = new List<HttpStatusCode>
            {
                HttpStatusCode.Unauthorized,
                HttpStatusCode.Forbidden,
                HttpStatusCode.NotFound
            };

            var domainValidation = statusCodes
                .Select(e => _validator.GetErrors().FirstOrDefault(x => x.StatusCode == e))
                .FirstOrDefault(e => e != null);

            if (domainValidation == null)
                return false;

            context.Result = new ObjectResult(new MessageResult(domainValidation.Message))
            {
                StatusCode = (int) domainValidation.StatusCode
            };

            return true;
        }
    }
}