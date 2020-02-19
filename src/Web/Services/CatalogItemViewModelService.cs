using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public class CatalogItemViewModelService : ICatalogItemViewModelService
    {
        private readonly IAsyncRepository<CatalogItem> _catalogItemRepository;
        private readonly ILogger<CatalogItemViewModel> _logger;

        public CatalogItemViewModelService(IAsyncRepository<CatalogItem> catalogItemRepository, ILoggerFactory logger)
        {
            _catalogItemRepository = catalogItemRepository;
            _logger = logger.CreateLogger<CatalogItemViewModelService>();
        }

        public async Task UpdateCatalogItem(CatalogItemViewModel viewModel)
        {
            _logger.LogInformation("UpdateCatalogItem called.");

            //Get existing CatalogItem
            var existingCatalogItem = await _catalogItemRepository.GetByIdAsync(viewModel.Id);
            if (existingCatalogItem == null)
            {
                _logger.LogError($"Catalog item not found. id={viewModel.Id}");
                throw new ModelNotFoundException($"Catalog item not found. id={viewModel.Id}");
            }

            //Build updated CatalogItem
            var updatedCatalogItem = existingCatalogItem;
            updatedCatalogItem.Name = viewModel.Name;
            updatedCatalogItem.Price = viewModel.Price;
            updatedCatalogItem.ShowPrice = viewModel.ShowPrice;
            updatedCatalogItem.CatalogBrandId = viewModel.CatalogBrandId;
            updatedCatalogItem.CatalogTypeId = viewModel.CatalogTypeId;

            await _catalogItemRepository.UpdateAsync(updatedCatalogItem);
            _logger.LogInformation($"Catalog item updated. id={viewModel.Id}");
        }

        public async Task DeleteCatalogItem(CatalogItemViewModel viewModel)
        {
            _logger.LogInformation("DeleteCatalogItem called.");

            var existingCatalogItem = await _catalogItemRepository.GetByIdAsync(viewModel.Id);
            if (existingCatalogItem == null)
            {
                _logger.LogInformation($"Catalog item not found. id={viewModel.Id}");
                throw new ModelNotFoundException($"Catalog item not found. id={viewModel.Id}");
            }

            await _catalogItemRepository.DeleteAsync(existingCatalogItem);
            _logger.LogInformation($"Catalog item updated. id={viewModel.Id}");
        }

        public async Task AddCatalogItem(CatalogItemViewModel viewModel)
        {
            _logger.LogInformation("AddCatalogItem called.");

            var newCatalogItem = new CatalogItem();

            newCatalogItem.Id = viewModel.Id;
            newCatalogItem.Name = viewModel.Name;
            // newCatalogItem.PictureUri = viewModel.PictureUri;
            newCatalogItem.Price = viewModel.Price;
            newCatalogItem.ShowPrice = viewModel.ShowPrice;
            newCatalogItem.CatalogBrandId = viewModel.CatalogBrandId;
            newCatalogItem.CatalogTypeId = viewModel.CatalogTypeId;

            await _catalogItemRepository.AddAsync(newCatalogItem);
            _logger.LogInformation($"Catalog item added. id={viewModel.Id}");
        }
    }
}
