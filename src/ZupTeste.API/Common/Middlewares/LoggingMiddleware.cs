using ZupTeste.API.Common.Middlewares.Extensions;

namespace ZupTeste.API.Common.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Action<string> _log;

        public LoggingMiddleware(RequestDelegate next, Action<string> log)
        {
            _next = next;
            _log = log;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = await context.GetRequestPresentationAsync();
            _log.Invoke(request);
            
            Stream SaveCopyOfOriginalBodyStream(out MemoryStream memoryStream)
            {
                var stream = context.Response.Body;
                memoryStream = new MemoryStream();
                context.Response.Body = memoryStream;
                return stream;
            }
            
            var originalBodyStream = SaveCopyOfOriginalBodyStream(out var responseBody);

            await _next(context);

            var response = await context.GetResponsePresentationAsync();
            _log.Invoke(response);

            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}