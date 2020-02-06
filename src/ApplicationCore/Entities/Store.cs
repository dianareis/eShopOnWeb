using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class Store : BaseEntity, IAggregateRoot
    {
        public string StoreName { get; set; }
    }
}