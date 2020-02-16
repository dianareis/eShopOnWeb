using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Admin
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class AddItemModel : PageModel
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly CatalogViewModelService _catalogViewModelService;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        

        public AddItemModel(ICatalogItemViewModelService catalogItemViewModelService, CatalogViewModelService catalogViewModelService, IAsyncRepository<CatalogItem> itemRepository)
        {
            _catalogItemViewModelService = catalogItemViewModelService;
            _catalogViewModelService = catalogViewModelService;
            _itemRepository = itemRepository;
        }

        [BindProperty]
        public CatalogItemViewModel CatalogModel { get; set; } = new CatalogItemViewModel();

        public async void OnGet()
        {

            CatalogModel.Brands = await _catalogViewModelService.GetBrands();
            CatalogModel.Types = await _catalogViewModelService.GetTypes();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var catalogItems = await _itemRepository.ListAllAsync();
                var lastItem = catalogItems.OrderBy(x => x.Id).LastOrDefault();
                var newId = lastItem.Id + 1;
                
                CatalogModel.Id = newId;
                await _catalogItemViewModelService.AddCatalogItem(CatalogModel);
            }

            return RedirectToPage("/Admin/Index");
        }
    }
}
