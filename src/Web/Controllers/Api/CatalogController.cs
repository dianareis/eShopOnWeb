using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    public class CatalogController : BaseApiController
    {
        private readonly ICatalogViewModelService _catalogViewModelService;

        public CatalogController(ICatalogViewModelService catalogViewModelService) => _catalogViewModelService = catalogViewModelService;

        [HttpGet]
        public async Task<ActionResult<CatalogIndexViewModel>> List(int? brandFilterApplied, int? typesFilterApplied, int? page, string searchText)
        {
            var itemsPage = 10;
            var catalogModel = await _catalogViewModelService.GetCatalogItems(page ?? 0, itemsPage, searchText, brandFilterApplied, typesFilterApplied, HttpContext.RequestAborted);
            return Ok(catalogModel);
        }
        

        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CatalogIndexViewModel>> GetById(int id)
        {
            try
            {
                var catalogItem = await _catalogViewModelService.GetItemById(id);
                return Ok(catalogItem);
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }

        // [HttpGet("{vista}")]
        // public ActionResult Vista(CatalogIndexViewModel catalogModel, string vista) {
        //     switch (vista){
        //         case "Grid":
        //             catalogModel.ResultView = ResultView.Grid;
        //             break;
        //         case "List":
        //             catalogModel.ResultView = ResultView.List;
        //             break;
        //         case "Table":
        //             catalogModel.ResultView = ResultView.Table;
        //             break;
        //         default:
        //             catalogModel.ResultView = ResultView.Grid;
        //             break;
        //     }
        //     return Ok(catalogModel.ResultView);
        // }
    }
}
