using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class StockPerStore : IAggregateRoot
    {
        public int ItemId {get; set; }       
        public int StoreId { get; set; }
        public int Stock {get; set; } = 0;

        public CatalogItem CatalogItem { get; set; }
        public Store Store { get; set; }
    }
}
