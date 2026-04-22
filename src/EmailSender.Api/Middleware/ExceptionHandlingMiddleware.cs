using System.Net;
using System.Text.Json;

namespace EmailSender.Api.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                await WriteErrorResponse(context, ex);
            }
        }

        private static Task WriteErrorResponse(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var payload = JsonSerializer.Serialize(new
            {
                error = "Ocorreu um erro inesperado.",
                detail = ex.Message
            });

            return context.Response.WriteAsync(payload);
        }
    }
}
