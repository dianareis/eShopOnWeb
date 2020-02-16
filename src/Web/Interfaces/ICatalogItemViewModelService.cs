using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Interfaces
{
    public interface ICatalogItemViewModelService
    {
        Task UpdateCatalogItem(CatalogItemViewModel viewModel);

        Task DeleteCatalogItem(CatalogItemViewModel viewModel);

        Task AddCatalogItem(CatalogItemViewModel viewModel);
    }
}
