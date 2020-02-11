using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.eShopWeb.Web.Middleware;

namespace Microsoft.eShopWeb.Web.Extensions.Middleware
{
    public static class RequestCultureMiddlewareExtension
    {
        public static void UseRequestCulture(this IApplicationBuilder app)
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("pt-PT"),
            };

            var options = new RequestLocalizationOptions
            { 
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures

            };
            
            options.RequestCultureProviders.Insert(0, new UrlRequestCultureProvider {
                Options = options
            });
            

            app.UseRequestLocalization(options);

        }
    }
}