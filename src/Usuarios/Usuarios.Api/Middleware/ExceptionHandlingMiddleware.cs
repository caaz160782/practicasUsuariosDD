using Microsoft.AspNetCore.Mvc;
using Usuarios.Application.Exceptions;

namespace Usuarios.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    public readonly RequestDelegate _next;
    public readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocurrio una excepcion: {ex.Message}");
            var exceptionDetail = GetExceptionDetail(ex);
            var problemDetail = new ProblemDetails
            {
                Status = exceptionDetail.Status,
                Type = exceptionDetail.Type,
                Title = exceptionDetail.Title,
                Instance = context.Request.Path
            };

            if (exceptionDetail.Errors is not null)
            {
                problemDetail.Extensions["errors"] = exceptionDetail.Errors;
            }

            context.Response.StatusCode = exceptionDetail.Status;
            await context.Response.WriteAsJsonAsync(problemDetail);
        }
    }

    private static ExceptionDetail GetExceptionDetail(Exception exception)
    {
        return exception switch
        {
            ValidationExceptions validationExceptions => new ExceptionDetail(
                Status: StatusCodes.Status400BadRequest,
                Type: "ValidacionFail",
                Title: "ValidacionFail",
                Detail: "Fallaron las validaciones",
                Errors: validationExceptions.Errors
            ),
            _ => new ExceptionDetail(
                Status: StatusCodes.Status500InternalServerError,
                Type: "Internal server error",
                Title: "Internal server error",
                Detail: "Internal server error",
                Errors: null
            )
        };
    }

    internal record ExceptionDetail
    (
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors
    );

}