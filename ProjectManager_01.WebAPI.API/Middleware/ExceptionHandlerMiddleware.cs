using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.Application.Exceptions;
using System.Net;

namespace ProjectManager_01.WebAPI.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ForbiddenException ex)
        {
            await HandleExceptionAsync(context, HttpStatusCode.Forbidden, "Access Denied", ex.Message);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, HttpStatusCode.NotFound, "Resource Not Found", ex.Message);
        }
        catch (OperationFailedException ex)
        {
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Operation Failed", ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Internal Server Error", ex.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string title, string detail)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = title,
            Detail = detail
        };

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}