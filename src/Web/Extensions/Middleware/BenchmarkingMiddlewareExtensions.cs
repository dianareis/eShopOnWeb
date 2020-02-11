using Microsoft.AspNetCore.Builder;
using Microsoft.eShopWeb.Web.Middleware;

namespace Web.Extensions.Middleware
{
    public static class BenchmarkingMiddlewareExtensions
    {
        public static void UseBenchmarking(this IApplicationBuilder app)
        {
            //app UseMiddleware<T>();
            app.UseMiddleware<MeasureRequestExecutionTime>();
        }
    }
}