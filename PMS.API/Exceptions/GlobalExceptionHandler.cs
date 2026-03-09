using System;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace PMS.API.Exceptions;

public class GlobalExceptionHandler
{
    public async Task HandleException(HttpContext httpContext)
    {
        var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();

        if (exceptionHandlerFeature == null) return;

        var exception = exceptionHandlerFeature.Error;

        httpContext.Response.ContentType = "application/json";

        var statusCode = GetStatusCode(exception);
        httpContext.Response.StatusCode = statusCode;

        var errorResponse = new ErrorResponse(statusCode, exception.Message, exception.GetType().Name);

        await httpContext.Response.WriteAsJsonAsync(JsonSerializer.Serialize(errorResponse));
    }

    private int GetStatusCode(Exception exception)
    {
        if (exception is ArgumentException)
        {
            return StatusCodes.Status400BadRequest;
        }

        else if (exception is UnauthorizedAccessException)
        {
            return StatusCodes.Status401Unauthorized;
        }

        else if (exception is KeyNotFoundException)
        {
            return StatusCodes.Status404NotFound;
        }

        else
        {
            return StatusCodes.Status500InternalServerError;
        }
    }
}
