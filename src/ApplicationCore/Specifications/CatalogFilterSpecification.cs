using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications
{

    public class CatalogFilterSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterSpecification(string searchText, int? brandId, int? typeId)
            : base(CatalogFilterPaginatedSpecification.BuildCatalogFilterExpression(searchText, brandId, typeId))
        {
        }
    }
}
