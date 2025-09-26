using System;

namespace PMS.API.Exceptions;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string ErrorType { get; set; }

    public ErrorResponse(int statusCode, string message, string errorType)
    {
        StatusCode = statusCode;
        Message = message;
        ErrorType = errorType;
    }
}
