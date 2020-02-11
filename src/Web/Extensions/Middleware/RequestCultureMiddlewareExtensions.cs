using Microsoft.AspNetCore.Builder;
using Microsoft.eShopWeb.Web.Middleware;

namespace Web.Extensions.Middleware
{
    public static class RequestCultureMiddlewareExtensions
    {
        public static void UseRequestCulture(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestCulture>();
        }
    }
}