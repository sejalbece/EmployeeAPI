using Interview_API.Exceptions;
using System.Net;
using System.Text.Json;

namespace Interview_API.Middleware
{
    public class GlobalExceptionMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware( RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            { 
                await _next(context);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex,"An error occurred.");
                if (ex is NotFoundException)
                {
                    await context.Response.WriteAsJsonAsync(new
                    {
                        statusCode = 400,
                        message = ex.Message
                    });
                }
                else
                {
                    await context.Response.WriteAsJsonAsync(new { 
                        statusCode = 500,
                        message = "An unhandled exception occurred."
                    });
                }


            }
        }

        
    }
}
