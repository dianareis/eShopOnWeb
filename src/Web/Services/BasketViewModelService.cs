using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IUriComposer _uriComposer;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly ILogger<BasketViewModelService> _logger;

        public BasketViewModelService(IAsyncRepository<Basket> basketRepository,
            IAsyncRepository<CatalogItem> itemRepository,
            IUriComposer uriComposer,
            ILoggerFactory loggerFactory)
        {
            _basketRepository = basketRepository;
            _uriComposer = uriComposer;
            _itemRepository = itemRepository;
            _logger = loggerFactory.CreateLogger<BasketViewModelService>();
        }

        public async Task<BasketViewModel> GetOrCreateBasketForUser(string userName)
        {
            var basketSpec = new BasketWithItemsSpecification(userName);
            var basket = (await _basketRepository.ListAsync(basketSpec)).FirstOrDefault();

            if (basket == null)
            {
                _logger.LogError($"ERROR Basket not found. {basket}");
                return await CreateBasketForUser(userName);
            }
            return await CreateViewModelFromBasket(basket);
        }

        private async Task<BasketViewModel> CreateViewModelFromBasket(Basket basket)
        {
            var viewModel = new BasketViewModel();
            viewModel.Id = basket.Id;
            viewModel.BuyerId = basket.BuyerId;
            viewModel.Items = await GetBasketItems(basket.Items); ;
            return viewModel;
        }

        private async Task<BasketViewModel> CreateBasketForUser(string userId)
        {
             _logger.LogInformation("CreateBasketForUser called.");
            var basket = new Basket() { BuyerId = userId };
            await _basketRepository.AddAsync(basket);

            return new BasketViewModel()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                Items = new List<BasketItemViewModel>()
            };
        }

        private async Task<List<BasketItemViewModel>> GetBasketItems(IReadOnlyCollection<BasketItem> basketItems)
        {
             _logger.LogInformation("GetBAsketItems called.");
            var items = new List<BasketItemViewModel>();
            foreach (var item in basketItems)
            {
                var itemModel = new BasketItemViewModel
                {
                    Id = item.Id,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    CatalogItemId = item.CatalogItemId
                };
                var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId);
                itemModel.PictureUrl = _uriComposer.ComposePicUri(catalogItem.PictureUri);
                itemModel.ProductName = catalogItem.Name;
                items.Add(itemModel);
            }

            return items;
        }
    }
}
