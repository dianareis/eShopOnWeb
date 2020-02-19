using System;
using System.Threading.Tasks;
using IronPdf;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.eShopWeb.Web.Controllers.Reports
{
    [Route("reports/[controller]/[action]")]
    public class PdfController : Controller
    {
        [HttpGet]
        public async Task<FileResult> Catalog(){
            string root = $"{Request.Scheme}://{Request.Host}/pdf";
            var uri = new Uri(root);
            var urlToPdf = new HtmlToPdf();
            var pdf = await urlToPdf.RenderUrlAsPdfAsync(uri);
            return File(pdf.BinaryData, "application/pdf;", "Catalog.pdf");
        }
    }
}