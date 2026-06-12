using EntertainmentTracker.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace EntertainmentTracker.API.Middleware
{
    public sealed class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(
                    context,
                    exception);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType =
                "application/json";

            var statusCode = exception switch
            {
                ValidationException => HttpStatusCode.BadRequest,

                UnauthorizedException => HttpStatusCode.Unauthorized,

                NotFoundException => HttpStatusCode.NotFound,

                ConflictException => HttpStatusCode.Conflict,

                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode =
                (int)statusCode;

            var response = new
            {
                Message = exception.Message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
