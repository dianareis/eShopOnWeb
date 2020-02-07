
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class StockPerStore : IAggregateRoot
    {
        public int ItemId {get; set; }
        public CatalogItem CatalogItems { get; set; }
        
        public int StoreId { get; set; }
        public Store Stores { get; set; }

        public int Stock {get; set; } = 0;
    }
}
