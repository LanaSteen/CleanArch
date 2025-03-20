using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MyApp.Application.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Middleware
{
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
                // Process the request further down the pipeline
                await _next(httpContext);
            }
            catch (NotFoundException ex)
            {
                // Log the exception and return a 404 response
                _logger.LogError(ex, ex.Message);
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsync($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // For any unhandled exceptions, log and return a 500 response
                _logger.LogError(ex, "An unexpected error occurred.");
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync("An unexpected error occurred.");
            }
        }
    }
}
