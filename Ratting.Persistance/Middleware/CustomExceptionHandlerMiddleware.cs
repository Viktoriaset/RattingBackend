using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Ratting.Aplication.Common.Exceptions;

namespace Ratting.Persistance.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate m_next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        m_next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await m_next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }   
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.Errors);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == String.Empty)
        {
            result = JsonSerializer.Serialize(new
            {
                errpr = exception.Message
            });
        }

        return context.Response.WriteAsync(result);
    }
}