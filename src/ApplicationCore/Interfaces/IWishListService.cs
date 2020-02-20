using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IWishListService
    {
        Task AddItemToWishList(int wishlistId, int catalogItemId);
        Task DeleteItemFromWishList(int wishlistId, int catalogItemId);
    }
}