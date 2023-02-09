using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using WebApi;
using WebApi.Models.Context;
using WebApi.Models.Entity;
using WebApi.Interfaces;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogService _logService;


    public LoggingMiddleware(RequestDelegate next, ILogService logService)
    {
        _next = next;
        _logService = logService;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        //We create a new log to save the action to the database
        using (var bookDbContext = new BookDbContext())
        {
            //Create a new log
            var log = new Log
            {   
                text = $"Action Invoked : {httpContext.Request.Path}",
                dateTime = DateTime.UtcNow
            };
            //Send it to the database by log service
            await _logService.CreateLog(log);
        }

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