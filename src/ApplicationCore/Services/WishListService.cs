using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Services
{
    public class WishListService : IWishListService
    {
        private readonly IAsyncRepository<WishList> _wishlistRepository;
        private readonly IAppLogger<WishListService> _logger;

        public WishListService(IAsyncRepository<WishList> wishlistRepository,
            IAppLogger<WishListService> logger)
        {
            _wishlistRepository = wishlistRepository;
            _logger = logger;
        }
        
        public async Task AddItemToWishList(int wishlistId, int catalogItemId)
        {
            var wishlist = await _wishlistRepository.GetByIdAsync(wishlistId);

            wishlist.AddItem(catalogItemId);

            await _wishlistRepository.UpdateAsync(wishlist);
        }

        public async Task DeleteItemFromWishList(int wishlistId, int catalogItemId)
        {
            var wishlist = await _wishlistRepository.GetByIdAsync(wishlistId);
            wishlist.DeleteItem(catalogItemId);

            await _wishlistRepository.UpdateAsync(wishlist);

        }

    }
}