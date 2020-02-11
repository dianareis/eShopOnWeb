using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.eShopWeb.Web.Middleware
{
    public class RequestCulture
    {
        public static void RequestCultureMiddleware() {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en"),
                new CultureInfo("pt-PT"),
            };

            var options = new RequestLocalizationOptions() {
                DefaultRequestCulture = new RequestCulture("en");
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures;
            }
        }
        
    }
}