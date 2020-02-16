using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Pages.Admin
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class EditCatalogItemModel : PageModel
    {
        private readonly ICatalogItemViewModelService _catalogItemViewModelService;
        private readonly CatalogViewModelService _catalogViewModelService;

        public EditCatalogItemModel(ICatalogItemViewModelService catalogItemViewModelService, CatalogViewModelService catalogViewModelService)
        {
            _catalogItemViewModelService = catalogItemViewModelService;
            _catalogViewModelService = catalogViewModelService;
        }

        [BindProperty]
        public CatalogItemViewModel CatalogModel { get; set; } = new CatalogItemViewModel();

        public async void OnGet(CatalogItemViewModel catalogModel)
        {
            CatalogModel = catalogModel;
            CatalogModel.Brands = await _catalogViewModelService.GetBrands();
            CatalogModel.Types = await _catalogViewModelService.GetTypes();
        }

        public async Task<IActionResult> OnPostAsync(string submitButton)
        {
            if (submitButton == "Save")
            {
                return await Edit();
            }
            else if (submitButton == "Delete")
            {
                return await Delete();
            }
            return RedirectToPage("/Admin/Index");
        }

        public async Task<IActionResult> Edit()
        {
            if (ModelState.IsValid)
            {
                await _catalogItemViewModelService.UpdateCatalogItem(CatalogModel);
            }

            return RedirectToPage("/Admin/Index");
        }
        public async Task<IActionResult> Delete()
        {
            if (ModelState.IsValid)
            {
                await _catalogItemViewModelService.DeleteCatalogItem(CatalogModel);
            }

            return RedirectToPage("/Admin/Index");
        }
    }
}
