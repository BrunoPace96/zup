using System.Net;
using MediatR;

namespace ZupTeste.DomainValidation.DataContracts
{
    public class DomainValidationNotification : INotification
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }
        public string Field { get; }

        public DomainValidationNotification(string message, string field)
        {
            StatusCode = 0;
            Message = message;
            Field = field;
        }
        
        public DomainValidationNotification(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}