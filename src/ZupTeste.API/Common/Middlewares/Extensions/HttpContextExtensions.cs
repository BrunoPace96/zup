using System.Text;

namespace ZupTeste.API.Common.Middlewares.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task<string> GetRequestPresentationAsync(this HttpContext context)
        {
            var request = context.Request;
            
            async Task<string> BodyStreamToJsonAsync()
            {
                request.EnableBuffering();
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);
                var bodyAsJson = Encoding.UTF8.GetString(buffer);
                request.Body.Position = 0;
                return bodyAsJson;
            }

            var json = await BodyStreamToJsonAsync();

            string BuildPresentation()
            {
                var builder = new StringBuilder();
                
                builder.AppendLine();
                builder.AppendLine("REQUEST");
                builder.AppendLine($"Method - {request.Method}");
                builder.Append($"{request.Scheme}://{request.Host}{request.Path}");

                if (!string.IsNullOrEmpty(request.QueryString.Value))
                    builder.Append($"{request.QueryString}");

                var authorizationHeader = request.Headers["Authorization"];
            
                builder.AppendLine();
                if(!string.IsNullOrEmpty(authorizationHeader))
                    builder.AppendLine(authorizationHeader);

                if (!string.IsNullOrEmpty(json))
                    builder.AppendLine(json);

                return builder.ToString();
            }

            return BuildPresentation();
        }
        
        public static async Task<string> GetResponsePresentationAsync(this HttpContext context)
        {
            var response = context.Response;
            
            async Task<string> BodyStreamToJsonAsync()
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                var bodyAsText = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
                return bodyAsText;
            }

            var json = await BodyStreamToJsonAsync();

            string BuildPresentation()
            {
                var builder = new StringBuilder();
                builder.AppendLine();

                builder.AppendLine("RESPONSE");
                builder.AppendLine($"StatusCode - {response.StatusCode}");

                if (!string.IsNullOrEmpty(json))
                    builder.AppendLine(json);
            
                builder.AppendLine();
                return builder.ToString();
            }

            return BuildPresentation();
        }
    }
}