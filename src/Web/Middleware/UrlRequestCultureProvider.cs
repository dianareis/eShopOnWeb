using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Microsoft.eShopWeb.Web.Middleware
{
    public class UrlRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            string cultureCode = null;

            // check if culture segment is on url
            if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value == "/")
            {
                cultureCode = this.GetDefaultCultureCode();
            }
            else if (httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.Length >= 4 && httpContext.Request.Path.Value[0] == '/' && httpContext.Request.Path.Value[3] == '/')
            {
                cultureCode = httpContext.Request.Path.Value.Substring(1, 2);

                if (!this.CheckCultureCode(cultureCode))
                {
                    cultureCode = this.GetDefaultCultureCode(); //throw new HttpException(HttpStatusCode.NotFound);
                }
        
            }
            else
            {
                cultureCode = this.GetDefaultCultureCode(); //throw new HttpException(HttpStatusCode.NotFound);
            }

            ProviderCultureResult requestCulture = new ProviderCultureResult(cultureCode);

            return Task.FromResult(requestCulture);
        }

        private string GetDefaultCultureCode()
        {
            return this.Options.DefaultRequestCulture.Culture.TwoLetterISOLanguageName;
        }

        private bool CheckCultureCode(string cultureCode)
        {
            return this.Options.SupportedCultures.Select(c => c.TwoLetterISOLanguageName).Contains(cultureCode);
        }
    }
}