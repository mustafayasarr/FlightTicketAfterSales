using FlightTicket.Domain.Constants;
using FlightTicket.Domain.Messages;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FlightTicket.API.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        await HandleAsync(exception, logger, httpContext);
        string code = string.Empty;
        if (exception is DivideByZeroException)
            code = ErrorMessages.DivideByZeroCode;
        else if (exception is NullReferenceException)
            code = ErrorMessages.NullReferenceExceptionCode;
        else if (exception is OutOfMemoryException)
            code = ErrorMessages.OutOfMemoryExceptionMessage;
        else if (exception is OverflowException)
            code = ErrorMessages.OverflowExceptionCode;
        else if (exception is InvalidCastException)
            code = ErrorMessages.InvalidCastExceptionCode;
        else if (exception is FormatException)
            code = ErrorMessages.FormatExceptionCode;
        else
            code= ErrorMessages.GlobalExceptionCode;

        var result = Result.Fail(exception.Message.ToString(),code);

        await httpContext.Response
            .WriteAsJsonAsync(result, cancellationToken);

        return true;
    }
    private async Task HandleAsync<T>(Exception exception, ILogger<T> logger, HttpContext httpContext)
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
