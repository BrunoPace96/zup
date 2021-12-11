using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ZupTeste.DomainValidation.Domain.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next
        )
        {
            _logger.LogInformation("Handling {CommandName}", typeof(TRequest).Name);
            var json = JsonConvert.SerializeObject(request, Formatting.Indented);
            _logger.LogInformation("{Json}", json);

            return await next();
        }
    }
}