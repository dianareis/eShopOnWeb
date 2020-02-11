using System.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Web.Pages;
using Microsoft.Extensions.Localization;

namespace Microsoft.eShopWeb.Web.Controllers
{
    public class HomeController : Controller
    {
         [HttpGet]
        public IActionResult Index()
        {
        return this.View();
        }
    }
}