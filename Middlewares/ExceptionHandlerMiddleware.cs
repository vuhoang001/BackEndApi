using System.Net;
using System.Text.RegularExpressions;
using BackEndApi.Exceptions;
using BackEndApi.Models.Common;
using Microsoft.Data.SqlClient;

namespace BackEndApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        if (ex.InnerException is SqlException sqlEx)
        {
            return HandleDatabaseExceptionAsync(context, sqlEx);
        }

        if (ex is not BusinessException businessException) return InternalServerError(context, ex);


        var errorResponse = new ErrorResponse()
        {
            Code = businessException.ErrorCode, HttpStatusCode = (int)businessException.StatusCode,
            Message = businessException.Message
        };
        context.Response.StatusCode = (int)businessException.StatusCode;

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
    }


    private static Task HandleDatabaseExceptionAsync(HttpContext context, SqlException sqlException)
    {
        var errResponse = new ErrorResponse()
        {
            Code = ExtractConstraintName(sqlException.Message),
            HttpStatusCode = (int)HttpStatusCode.BadRequest,
            Message = sqlException.Message,
        };

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errResponse));
    }


    private static string ExtractConstraintName(string errorMessage)
    {
        var match = Regex.Match(errorMessage, @"constraint\s+['""]?([\w\d_]+)['""]?", RegexOptions.IgnoreCase);
        return match.Success ? match.Groups[1].Value.ToUpper() : "UNKNOWN_ERROR";
    }

    private static Task InternalServerError(HttpContext context, Exception ex)
    {
        var defaultErrorResponse = new ErrorResponse()
        {
            Code = "INTERNAL_ERROR", HttpStatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "Internal Server Error - Please contact the administrator.",
            MessageDev = ex.Message,
        };
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(defaultErrorResponse));
    }
}