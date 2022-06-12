using System.Net;
using System.Text.Json;
using Antivirus.Application.Common.Exceptions;

namespace Antivirus.API.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="next"> Next request delegate. </param>
    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invoke.
    /// </summary>
    /// <param name="context"> Current HttpContext. </param>
    /// <returns> Task. </returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Handles exceptions, writes corresponding status code.
    /// </summary>
    /// <param name="context"> Current HttpContext. </param>
    /// <param name="ex"> Exception. </param>
    /// <returns> Task. </returns>
    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (ex)
        {
            case ArgumentException:
                code = HttpStatusCode.BadRequest;
                break;
            case ScanNotFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "Application/json";
        context.Response.StatusCode = (int)code;
        if (string.IsNullOrEmpty(result))
        {
            result = JsonSerializer.Serialize(new { errors = new[] { ex.Message } });
        }

        return context.Response.WriteAsync(result);
    }
}