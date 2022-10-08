using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestProject.WebAPI.Common;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Extensions
{
    public class ApiWrapper
    {
        private readonly RequestDelegate _next;

        public ApiWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger logger)
        {
            string bodyText = await new StreamReader(context.Request.Body).ReadToEndAsync();            

            await logger.LogRequestAsync(context.Request);

            Stream originalBody = context.Response.Body;

            using (MemoryStream responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await _next.Invoke(context);

                    //Handle Response
                    string body = await FormatResponseAsync(context.Response);

                    //Success Response
                    if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                    {
                        await HandleSuccessAsync(context, body, context.Response.StatusCode, logger);
                    }
                    //Rest of Status code
                    else
                    {
                        await HandleFailureAsync(context, context.Response.StatusCode, logger);
                    }
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(context, ex, logger);
                }
                finally
                {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBody);
                }
            }
        }

        private async Task<string> FormatResponseAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }

        private static async Task HandleSuccessAsync(HttpContext context, string body, int code, ILogger logger)
        {
            string type = "Success";
            string message = "Success";
            ApiResponseFormat apiResponse = null;
            string jsonString;

            context.Response.ContentType = "application/json";
            dynamic bodyContent = JsonConvert.DeserializeObject<dynamic>(body);
            apiResponse = new ApiResponseFormat(type, code, message, bodyContent, false);
            jsonString = JsonConvert.SerializeObject(apiResponse, Formatting.Indented);

            await context.Response.WriteAsync(jsonString);
        }

        private static async Task HandleFailureAsync(HttpContext context, int code, ILogger logger)
        {
            context.Response.ContentType = "application/json";
            string type = null;
            string message = null;
            ApiResponseFormat apiResponse = null;

            switch (code)
            {
                case (int)HttpStatusCode.NotFound:
                    {
                        type = HttpStatusCode.NotFound.ToString();
                        message = "The specified URI/Resource does not exists. Please verify and try again.";
                    }
                    break;
                case (int)HttpStatusCode.Unauthorized:
                    {
                        type = HttpStatusCode.Unauthorized.ToString();
                        message = "You are unauthorised to access the requested resource.";
                    }
                    break;
                case (int)HttpStatusCode.Forbidden:
                    {
                        type = HttpStatusCode.Forbidden.ToString();
                        message = "Your account is not authorised to access the requested resource.";
                    }
                    break;
                case (int)HttpStatusCode.NoContent:
                    {
                        type = HttpStatusCode.NoContent.ToString();
                        message = "The specified URI does not contain any content.";
                    }
                    break;
                case (int)HttpStatusCode.TooManyRequests:
                    {
                        type = HttpStatusCode.TooManyRequests.ToString();
                        message = "Rate limit exceeded.";
                    }
                    break;
                case (int)HttpStatusCode.BadRequest:
                    {
                        type = HttpStatusCode.BadRequest.ToString();
                        message = "Request is not valid.";
                    }
                    break;
                default:
                    {
                        type = "Failure";
                        message = "Your request cannot be processed. Please contact support.";
                    }
                    break;
            }

            apiResponse = new ApiResponseFormat(type, code, message, null, false);
            context.Response.StatusCode = code;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse, Formatting.Indented));

        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            string type = null;
            int code = 0;
            string message = "Unknown Error!";
            ApiResponseFormat apiResponse = null;


            if (exception is UnauthorizedAccessException)
            {
                code = (int)HttpStatusCode.Unauthorized;
                context.Response.StatusCode = code;
                type = HttpStatusCode.Unauthorized.ToString();
                message = exception.Message;
            }
            else
            {
                code = (int)HttpStatusCode.InternalServerError;
                context.Response.StatusCode = code;
                type = HttpStatusCode.InternalServerError.ToString();
                message = exception.Message;
            }

            await logger.LogErrorAsync(exception, Severity.Error);

            apiResponse = new ApiResponseFormat(type, code, message, null, true);
            context.Response.StatusCode = code;

            await context.Response.WriteAsync(JsonConvert.SerializeObject(apiResponse, Formatting.Indented));

        }
    }
}
