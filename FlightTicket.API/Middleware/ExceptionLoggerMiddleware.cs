using System.Text;

namespace FlightTicket.API.Middleware
{
    public class ExceptionLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<ExceptionLoggerMiddleware> logger)
        {
            try
            {
                httpContext.Request.EnableBuffering();
                await _next.Invoke(httpContext);
            }
            catch (Exception e)
            {
                await HandleAsync(e, logger, httpContext);
            }
        }
        private async Task HandleAsync(Exception exception, ILogger<ExceptionLoggerMiddleware> logger, HttpContext httpContext)
        {
                string requestAsText = await RequestAsTextAsync(httpContext);
                logger.LogError(exception, "{RequestAsText}{NewLine1}{NewLine2}{ExceptionMessage}", requestAsText, Environment.NewLine, Environment.NewLine, exception.Message);
        }

        private static async Task<string> RequestAsTextAsync(HttpContext httpContext)
        {
            string rawRequestBody = await GetRawBodyAsync(httpContext.Request);

            IEnumerable<string> headerLine = httpContext.Request.Headers.Where(h => h.Key != "Authentication").Select(pair => $"{pair.Key} => {string.Join("|", pair.Value.ToList())}");
            string headerText = string.Join(Environment.NewLine, headerLine);

            string message =
              $"Request: {httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}{httpContext.Request.QueryString}{Environment.NewLine}" +
              $"Headers: {Environment.NewLine}{headerText}{Environment.NewLine}" +
              $"Content : {Environment.NewLine}{rawRequestBody}";

            return message;
        }

        private static async Task<string> GetRawBodyAsync(HttpRequest request, Encoding? encoding = null)
        {
            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);
            string body = await reader.ReadToEndAsync().ConfigureAwait(false);
            request.Body.Position = 0;

            return body;
        }
    }
}
