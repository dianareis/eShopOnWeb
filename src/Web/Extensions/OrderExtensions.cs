using System.Linq;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Extensions
{
    public static class OrderExtensions
    {
        /// <summary>
        /// Create order view model
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Order view model</returns>
        public static OrderViewModel CreateViewModel(this Order order) {
            return new OrderViewModel
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems?.Select(oi => new OrderItemViewModel()
                {
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = order.Id,
                BuyerId = order.BuyerId,
                Status = order.Status,
                Notes = order.Notes,
                ShippingAddress = order.ShipToAddress,
                Total = order.Total()
            };
        }
    }
}