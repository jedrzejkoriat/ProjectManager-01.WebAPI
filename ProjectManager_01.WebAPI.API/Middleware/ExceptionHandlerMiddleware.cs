using Microsoft.AspNetCore.Mvc;

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
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Status = 500,
                Title = "Internal Server Error",
                Detail = ex.Message
            };

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
