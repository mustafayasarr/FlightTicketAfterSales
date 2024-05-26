using FlightTicket.Domain.Messages;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightTicket.API.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        var result = Result.Fail(exception.Message.ToString());

        await httpContext.Response
            .WriteAsJsonAsync(result, cancellationToken);

        return true;
    }
}
