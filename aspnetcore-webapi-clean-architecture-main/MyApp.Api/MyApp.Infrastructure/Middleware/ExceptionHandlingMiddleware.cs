using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException validationException)
        {
            _logger.LogError($"Validation failed: {validationException.Message}");
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest; // 400 Bad Request
            await httpContext.Response.WriteAsync($"Validation failed: {validationException.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");

        
            if (ex is ArgumentException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest; // 400 Bad Request
                await httpContext.Response.WriteAsync($"Bad Request: {ex.Message}");
            }
            else if (ex is KeyNotFoundException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound; // 404 Not Found
                await httpContext.Response.WriteAsync($"Not Found: {ex.Message}");
            }
            else
            {
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError; // 500 Internal Server Error
                await httpContext.Response.WriteAsync($"Internal Server Error: {ex.Message}");
            }
        }
    }
}
