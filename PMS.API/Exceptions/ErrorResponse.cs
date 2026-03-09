using System;

namespace PMS.API.Exceptions;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    public string ErrorType { get; set; }

    public ErrorResponse(int statusCode, string errorMessage, string errorType)
    {
        StatusCode = statusCode;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }

}
