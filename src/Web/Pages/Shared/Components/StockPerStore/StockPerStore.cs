using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Pages.Shared.Components.StockPerStore
{
    public class StockPerStore : ViewComponent
    {
        private readonly ILogger<StockPerStoreViewModelService> _logger;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly IAsyncRepository<Store> _storeRepository;
        private readonly CatalogContext _catalogContext;

        public StockPerStore( ILoggerFactory loggerFactory,
            IAsyncRepository<CatalogItem> itemRepository,
            IAsyncRepository<Store> storeRepository,
            CatalogContext catalogContext)
        {
            _logger = loggerFactory.CreateLogger<StockPerStoreViewModelService>();
            _itemRepository = itemRepository;
            _storeRepository = storeRepository;
            _catalogContext = catalogContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int catalogItemId)
        {
            var items = await GetStockPerStore(catalogItemId);
            return View(items);
        }

        public async Task<List<StockPerStoreViewModel>> GetStockPerStore(int catalogItemId) {

            var item = await _itemRepository.GetByIdAsync(catalogItemId);
            if (item == null)
            {
                throw new ModelNotFoundException($"Catalog item not found. id={catalogItemId}");
            }

            var query = _catalogContext.StockPerStore.Where(stockPerStore => stockPerStore.ItemId == catalogItemId).ToList();

            var lista = new List<StockPerStoreViewModel>();

            foreach(var i in query) {
                var stockModel = new StockPerStoreViewModel()
                {
                    Stock = i.Stock,
                    StoreId = i.StoreId
                };

                lista.Add(stockModel);
            }
            return lista;
        }
    }
}