using System.Net;
using FastDeliveryAPI.Exceptions;
using Newtonsoft.Json;

namespace FastDeliveryAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            _logger.LogError(ex, $"Ocurrio un error en el proceso {context.Request.Path}");
            await HandleExceptionAsync(context, ex);
        }
    }

    public Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        var errorDetails = new ErrorDetails
        {
            ErrorType = "Failure",
            ErrorMessage = ex.Message
        };

        switch (ex)
        {
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorDetails.ErrorType = "No found";
                break;
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                errorDetails.ErrorType = "BadRequest";
                break;
            case CreditLimitException creditLimitException:
                statusCode = HttpStatusCode.BadRequest;
                errorDetails.ErrorType = "BadRequest";
                break;
            case EmailException emailException:
                statusCode = HttpStatusCode.BadRequest;
                errorDetails.ErrorType = "Error Email";
                break;
            default:
                break;
        }

        string response = JsonConvert.SerializeObject(errorDetails);

        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(response);
    }

    public class ErrorDetails
    {
        public string? ErrorType { get; set; }
        public string? ErrorMessage { get; set; }
    }

}