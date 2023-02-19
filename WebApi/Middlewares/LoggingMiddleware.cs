using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using WebApi;
using WebApi.Models.Context;
using WebApi.Models.Entity;
using WebApi.Interfaces;
using BookStore.Api.Interfaces;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
        

    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        
        //Create a new log
        Console.WriteLine($"{DateTime.UtcNow} Action Invoked : {httpContext.Request.Path}");

        await _next(httpContext);
    }
}

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}