using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.eShopWeb.Web.Middleware;

namespace Microsoft.eShopWeb.Web.Extensions.Middleware
{
    public static class RequestCultureMiddlewareExtension
    {
        public static void UseRequestCulture(this IApplicationBuilder app)
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("pt-PT"),
            };

            var options = new RequestLocalizationOptions()
            { 
                DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures

            };

            options.RequestCultureProviders = new List<IRequestCultureProvider>()  
            {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider(),
                new RouteDataRequestCultureProvider() { Options = options } 
            };
            
            options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());

            app.UseRequestLocalization(options);
        }
    }
}