using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.Pages.WishList;

namespace Microsoft.eShopWeb.Web.Interfaces
{
    public interface IWishListViewModelService
    {
         Task<WishListViewModel> GetOrCreateWishListForUser(string userName);
    }
}