using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities
{
    public class Lojas : BaseEntity, IAggregateRoot
    {
        public string Loja { get; set; }
    }
}