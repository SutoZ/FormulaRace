using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TeamManagementService.Application.Middleware;

public class DBUpdateExceptionHandler(ILogger<DBUpdateExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception occurred while processing request.");

        httpContext.Response.ContentType = "application/json";

        if (exception is DbUpdateException)
        {
            logger.LogError(exception, "Database update exception occurred. {Message}", exception.Message);

            ProblemDetails problemDetails = new()
            {
                Title = "Database Update Error",
                Detail = exception.Message,
                Status = StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
        return false;
    }
}