
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.eShopWeb.Web.Features.AdminOrders;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Pages.Admin.Orders
{
    [Authorize(Roles = AuthorizationConstants.Roles.ADMINISTRATORS)]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IMediator mediator, IOrderRepository orderRepository, ILogger<IndexModel> logger)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
            _logger = logger;
        }
        

        public IEnumerable<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
        public IEnumerable<SelectListItem> OrderStatusList { get; set; }

        public async Task OnGet(string buyerId = null,
            DateTimeOffset? createdBefore = null,
            DateTimeOffset? createdAfterB = null)
        {
            Orders = await _mediator.Send(new GetAdminOrders());
            
            OrderStatusList = Enum<OrderStatus>.GetAll()
                .Select(orderStatus => new SelectListItem { Value = orderStatus.ToString(), Text = orderStatus.ToString() });
        }

        public async Task<IActionResult> OnPost(OrderViewModel viewModel)
        {
            var existingOrder = await _orderRepository.GetByIdWithItemsAsync(viewModel.OrderNumber);

            var updatedOrder = existingOrder;
            // OrderStatus updatedStatus;
            // Enum.TryParse<OrderStatus>(viewModel.Status.ToString(), out updatedStatus);
            updatedOrder.Status = viewModel.Status;
            updatedOrder.Notes = viewModel.Notes;

            await _orderRepository.UpdateAsync(updatedOrder);
            return Redirect("~/Admin/Orders");
        }
    }
}
