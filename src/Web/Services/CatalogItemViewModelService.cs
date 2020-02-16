using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public class CatalogItemViewModelService : ICatalogItemViewModelService
    {
        private readonly IAsyncRepository<CatalogItem> _catalogItemRepository;

        public CatalogItemViewModelService(IAsyncRepository<CatalogItem> catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        public async Task UpdateCatalogItem(CatalogItemViewModel viewModel)
        {
            //Get existing CatalogItem
            var existingCatalogItem = await _catalogItemRepository.GetByIdAsync(viewModel.Id);

            //Build updated CatalogItem
            var updatedCatalogItem = existingCatalogItem;
            updatedCatalogItem.Name = viewModel.Name;
            updatedCatalogItem.Price = viewModel.Price;
            updatedCatalogItem.ShowPrice = viewModel.ShowPrice;
            updatedCatalogItem.CatalogBrandId = viewModel.CatalogBrandId;
            updatedCatalogItem.CatalogTypeId = viewModel.CatalogTypeId;


            await _catalogItemRepository.UpdateAsync(updatedCatalogItem);
        }

        public async Task DeleteCatalogItem(CatalogItemViewModel viewModel)
        {
            var existingCatalogItem = await _catalogItemRepository.GetByIdAsync(viewModel.Id);

            await _catalogItemRepository.DeleteAsync(existingCatalogItem);
        }

        public async Task AddCatalogItem(CatalogItemViewModel viewModel)
        {
            var newCatalogItem = new CatalogItem();

            newCatalogItem.Id = viewModel.Id;
            newCatalogItem.Name = viewModel.Name;
            // newCatalogItem.PictureUri = viewModel.PictureUri;
            newCatalogItem.Price = viewModel.Price;
            newCatalogItem.ShowPrice = viewModel.ShowPrice;
            newCatalogItem.CatalogBrandId = viewModel.CatalogBrandId;
            newCatalogItem.CatalogTypeId = viewModel.CatalogTypeId;

            await _catalogItemRepository.AddAsync(newCatalogItem);
        }
    }
}
