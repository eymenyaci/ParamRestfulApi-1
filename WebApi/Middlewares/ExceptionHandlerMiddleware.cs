using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using WebApi.Interfaces;
using WebApi.Models.Entity;
using WebApi.Extensions;

namespace WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly ILogService _logService;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, ILogService logService)
        {
            this.next = next;
            this.logger = logger;
            _logService = logService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //When the application runs correctly, the try inside runs successfully. If we get an error, we will catch the error here before the controller goes.
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                //error handling done here
                logger.LogError(ex.Message + "Path : " + httpContext.Request.Path);

            }
        }

    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}