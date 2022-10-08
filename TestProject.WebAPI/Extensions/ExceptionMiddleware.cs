using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.Interfaces;

namespace TestProject.WebAPI.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogger logger)
        {
            try
            {
                await logger.LogRequestAsync(httpContext.Request);
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await logger.LogErrorAsync(ex, Severity.Error);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Something went wrong. Please try again after sometime."
            }.ToString());
        }
    }
}
