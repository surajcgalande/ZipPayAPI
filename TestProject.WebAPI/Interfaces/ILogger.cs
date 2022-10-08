using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using TestProject.WebAPI.Common;

namespace TestProject.WebAPI.Interfaces
{
    public interface ILogger
    {
        Task LogErrorAsync(Exception exception, Severity Severity);
        Task LogRequestAsync(HttpRequest request);
    }
}
