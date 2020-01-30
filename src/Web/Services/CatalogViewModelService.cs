﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    /// <summary>
    /// This is a UI-specific service so belongs in UI project. It does not contain any business logic and works
    /// with UI-specific types (view models and SelectListItem types).
    /// </summary>
    public class CatalogViewModelService : ICatalogViewModelService
    {
        private readonly ILogger<CatalogViewModelService> _logger;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly IAsyncRepository<CatalogBrand> _brandRepository;
        private readonly IAsyncRepository<CatalogType> _typeRepository;
        private readonly IUriComposer _uriComposer;
        private readonly ICurrencyService _currencyService;
        private readonly CatalogContext _catalogContext;

        private const Currency DEFAULT_PRICE_UNIT = Currency.USD;
        private const Currency USER_PRICE_UNIT = Currency.EUR;

        public CatalogViewModelService(
            ILoggerFactory loggerFactory,
            IAsyncRepository<CatalogItem> itemRepository,
            IAsyncRepository<CatalogBrand> brandRepository,
            IAsyncRepository<CatalogType> typeRepository,
            IUriComposer uriComposer,
            ICurrencyService currencyService,
            CatalogContext catalogContext)
        {
            _logger = loggerFactory.CreateLogger<CatalogViewModelService>();
            _itemRepository = itemRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _uriComposer = uriComposer;
            _currencyService = currencyService;
            _catalogContext = catalogContext;
        }

        private async Task<CatalogItemViewModel> CreateCatalogItemViewModel(CatalogItem catalogItem, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new CatalogItemViewModel()
            {
                Id = catalogItem.Id,
                Name = catalogItem.Name,
                PictureUri = catalogItem.PictureUri,
                Price = await _currencyService.Convert(catalogItem.Price, DEFAULT_PRICE_UNIT, USER_PRICE_UNIT, cancellationToken),
                ShowPrice = catalogItem.ShowPrice,
                PriceUnit = USER_PRICE_UNIT
            };
        }


        private async Task<IReadOnlyList<CatalogItem>> ListCatalogItem(int pageItemsOffset, int itemsPage, int? brandId, int? typeId)
        {
            var query = _catalogContext.CatalogItems as IQueryable<CatalogItem>;
            var whereExp = new List<Expression<Func<CatalogItem, bool>>>();
            if (brandId.HasValue & typeId.HasValue)
            {
                // query = query.Where(x => x.CatalogBrandId == brandId.Value & x.CatalogTypeId == typeId.Value);
                whereExp.Add(x => x.CatalogBrandId == brandId.Value & x.CatalogTypeId == typeId);
            }
            else if (brandId.HasValue)
            {
                query = query.Where(x => x.CatalogBrandId == brandId.Value);
                whereExp.Add(x => x.CatalogBrandId == brandId.Value);
            }
            else if (typeId.HasValue)
            {
                // query = query.Where(x => x.CatalogTypeId == typeId.Value);
                whereExp.Add(x => x.CatalogTypeId == typeId.Value);
            }
            whereExp.ForEach(expr => query.Where(expr));
            query.Skip(pageItemsOffset).Take(itemsPage);
            return await query.ToListAsync();
        }

        private Task<int> CountCatalogItems(int? brandId, int? typeId)
        {
            var query = _catalogContext.CatalogItems as IQueryable<CatalogItem>;
            if (brandId.HasValue & typeId.HasValue)
            {
                query = query.Where(x => x.CatalogBrandId == brandId.Value & x.CatalogTypeId == typeId.Value);

            } else if (brandId.HasValue)
            {
                query = query.Where(x => x.CatalogBrandId == brandId.Value);

            } else if (typeId.HasValue)
            {
                query = query.Where(x => x.CatalogTypeId == typeId.Value);

            }

            return query.CountAsync();

        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, string searchText, int? brandId, int? typeId, CancellationToken cancellationToken = default(CancellationToken))
        {
            _logger.LogInformation("GetCatalogItems called.");

            var filterSpecification = new CatalogFilterSpecification(searchText, brandId, typeId);
            var filterPaginatedSpecification = new CatalogFilterPaginatedSpecification(itemsPage * pageIndex, itemsPage, searchText, brandId, typeId);

            // the implementation below using ForEach and Count. We need a List.
            var itemsOnPage = await _itemRepository.ListAsync(filterPaginatedSpecification);
            var totalItems = await _itemRepository.CountAsync(filterSpecification);

            // var itemsOnPage = await ListCatalogItem(itemsPage * pageIndex, itemsPage, brandId, typeId);
            // var totalItems = await CountCatalogItems(brandId, typeId);

            foreach (var itemOnPage in itemsOnPage)
            {
                itemOnPage.PictureUri = _uriComposer.ComposePicUri(itemOnPage.PictureUri);
            }

            var CatalogItemsTask = Task.WhenAll(itemsOnPage.Select(catalogItem => CreateCatalogItemViewModel(catalogItem, cancellationToken)));
            cancellationToken.ThrowIfCancellationRequested();
            var vm = new CatalogIndexViewModel()
            {
                CatalogItems = await CatalogItemsTask,
                Brands = await GetBrands(cancellationToken),
                Types = await GetTypes(cancellationToken),
                BrandFilterApplied = brandId ?? 0,
                TypesFilterApplied = typeId ?? 0,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands(CancellationToken cancellationToken = default(CancellationToken))
        {
            _logger.LogInformation("GetBrands called.");
            var brands = await _brandRepository.ListAllAsync();

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (CatalogBrand brand in brands)
            {
                items.Add(new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand });
            }

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes(CancellationToken cancellationToken = default(CancellationToken))
        {
            _logger.LogInformation("GetTypes called.");

            // var types = await _catalogContext.CatalogItems.ToListAsync();
            var types = await _typeRepository.ListAllAsync();
            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (CatalogType type in types)
            {
                items.Add(new SelectListItem() { Value = type.Id.ToString(), Text = type.Type });
            }

            return items;
        }

        public async Task<CatalogItemViewModel> GetItemById(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var item = await _itemRepository.GetByIdAsync(id);
                if (item == null)
                {
                    throw new ModelNotFoundException($"Catalog item not found. id={id}");
                }
                var catalogItemViewModel = await CreateCatalogItemViewModel(item, cancellationToken);
                return catalogItemViewModel;
            }
            catch (Exception ex)
            {
                throw new ModelNotFoundException($"Catalog item not found. id={id}", ex);
            }
        }
    }

    [Serializable]
    internal class ModelNotFoundException : Exception
    {
        public ModelNotFoundException()
        {
        }

        public ModelNotFoundException(string message) : base(message)
        {
        }

        public ModelNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModelNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
