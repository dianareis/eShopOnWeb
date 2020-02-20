
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.eShopWeb.Web.Features.AllOrders;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Pages.Admin.Orders
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;

        public IndexModel(IMediator mediator, IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
        }
        

        public IEnumerable<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
        public IEnumerable<SelectListItem> OrderStatus { get; set; }

        public async Task OnGet(string buyerId = null,
            DateTimeOffset? createdBefore = null,
            DateTimeOffset? createdAfterB = null)
        {
            Orders = await _mediator.Send(new GetAdminOrders());
            OrderStatus = Enum<OrderStatus>.GetAll()
                .Select(orderStatus => new SelectListItem { Value = orderStatus.ToString(), Text = orderStatus.ToString() });
        }

        public async Task OnPost(int orderNumber)
        {
            var existingOrder = _orderRepository.GetByIdAsync(orderNumber);

            // var updatedOrder = existingOrder;
            // updatedOrder.Status = viewModel.Name;
            // updatedCatalogItem.Price = viewModel.Price;
            // updatedCatalogItem.ShowPrice = viewModel.ShowPrice;
            // updatedCatalogItem.CatalogBrandId = viewModel.CatalogBrandId;
            // updatedCatalogItem.CatalogTypeId = viewModel.CatalogTypeId;

            // await _catalogItemRepository.UpdateAsync(updatedCatalogItem);
        }
    }
}
