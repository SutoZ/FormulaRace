using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Text.Json;

namespace TeamManagementService.Application.Middleware;

//Refactor to be general and not specific
public class ExceptionHandler(ILogger logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.Error(exception, "Unhandled exception occurred while processing request.");

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = exception switch
        {
            ArgumentNullException or ArgumentException or ArgumentOutOfRangeException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            InvalidOperationException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        var result = JsonSerializer.Serialize(new
        {
            error = exception.Message,
            status = httpContext.Response.StatusCode
        });

        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true; // Exception was handled
    }
}