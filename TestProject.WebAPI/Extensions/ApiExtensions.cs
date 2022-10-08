using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject.WebAPI.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApiWrapper(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseApiWrapper(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiWrapper>();
            return app;
        }
    }
}
