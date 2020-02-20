using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate
{
    public class WishListItem : BaseEntity, IAggregateRoot
    {
        public string BuyerId {get; set; }
        public decimal UnitPrice { get; set; }
        public int CatalogItemId { get; set; }
    }
}