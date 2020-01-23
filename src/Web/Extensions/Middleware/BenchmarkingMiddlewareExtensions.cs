using Microsoft.AspNetCore.Builder;
using Web.Middleware;

namespace Web.Extensions.Middleware
{
    public static class BenchmarkingMiddlewareExtensions
    {
        public static IApplicationBuilder UseBenchmarking(this IApplicationBuilder app)
        {
            //app UseMiddleware<T>();
            return app.UseMiddleware<MeasureRequestExecutionTime>();
        }
    }
}