using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class Store : BaseEntity, IAggregateRoot
    {
        public string StoreName { get; set; }
        public List<StockPerStore> StockPerStore { get; set;  }
    }
}