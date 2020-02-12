using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class DefaultController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }
    }
}