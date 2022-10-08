using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TestProject.WebAPI.Interfaces;
using TestProject.WebAPI.Models;

namespace TestProject.WebAPI.Common
{
    public class Logger : ILogger
    {
        private readonly IAppDBContext _appDBContext;

        public Logger(IAppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task LogErrorAsync(Exception exception, Severity Severity)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"Message : {exception.Message}{Environment.NewLine}");
            stringBuilder.Append($"Inner Exception : {exception.InnerException?.Message ?? ""}{Environment.NewLine}");
            stringBuilder.Append($"Stack Trace : {exception.StackTrace}{Environment.NewLine}");

            await _appDBContext.ErrorLog.AddAsync(new Log { CreatedDate = DateTime.Now, Severity = (int)Severity, Message = stringBuilder.ToString() });
            await _appDBContext.SaveChangesAsync();
        }

        public async Task LogRequestAsync(HttpRequest request)
        {
            StringBuilder stringBuilder = new StringBuilder();

            string text = await new StreamReader(request.Body).ReadToEndAsync();

            if (request.Body.CanSeek) request.Body.Seek(0, SeekOrigin.Begin);

            stringBuilder.Append($"Method : {request.Method}{Environment.NewLine}");
            stringBuilder.Append($"Type : {request.ContentType ?? "application/json"}{Environment.NewLine}");
            stringBuilder.Append($"URL : {request.Scheme}://{request.Host}{request.Path}{request.QueryString}{Environment.NewLine}");
            stringBuilder.Append($"Body : {text?.Replace(Environment.NewLine, "")}");

            await _appDBContext.ErrorLog.AddAsync(new Log { CreatedDate = DateTime.Now, Severity = (int)Severity.Info, Message = stringBuilder.ToString() });
        }
    }
}
