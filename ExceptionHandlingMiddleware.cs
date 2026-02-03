using System.Net;
using System.Reflection;
using System.Text.Json;

namespace SmartEnum;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(
                ex.InnerException ?? ex,
                "Unhandled exception occurred. TraceId: {TraceId}",
                context.TraceIdentifier);

            await HandleExceptionAsync(context, ex.InnerException ?? ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var (statusCode, title) = exception switch
        {
            ArgumentException =>
                (HttpStatusCode.BadRequest, "Invalid request"),

            UnauthorizedAccessException =>
                (HttpStatusCode.Unauthorized, "Unauthorized"),

            KeyNotFoundException =>
                (HttpStatusCode.NotFound, "Resource not found"),

            TargetInvocationException =>
                (HttpStatusCode.BadRequest, "Invalid operation."),

            JsonException => (HttpStatusCode.BadRequest, "Invalid json operation."),
            _ =>
                (HttpStatusCode.InternalServerError, "Internal server error")
        };

        var problemDetails = new
        {
            type = $"https://httpstatuses.com/{(int)statusCode}",
            title,
            status = (int)statusCode,
            detail = exception.Message,
            traceId = context.TraceIdentifier
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(problemDetails));
    }
}
