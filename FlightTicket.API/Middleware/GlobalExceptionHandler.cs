using FlightTicket.Domain.Messages;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightTicket.API.Middleware
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = exception.GetType().Name,
                Title = exception.Message,
                Detail = exception.InnerException?.ToString(),
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            };

            _logger.LogError($"Exception occurred: {JsonConvert.SerializeObject(problemDetails)}");

            var result = Result.Fail(exception.Message.ToString());

            await httpContext.Response
                .WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}
