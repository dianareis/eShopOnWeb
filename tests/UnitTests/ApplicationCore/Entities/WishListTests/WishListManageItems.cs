using System;
using System.Linq;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.ApplicationCore.Entities.WishListTests
{
    public class WishListManageItems
    {
        private int _testCatalogItemId = 123;

        [Fact]
        public void AddWishListItem()
        {
            var wishList = new WishList();
            wishList.AddItem(_testCatalogItemId);

            var firstItem = wishList.Items.Single();
            Assert.Equal(_testCatalogItemId, firstItem.CatalogItemId);
        }

        [Fact]
        public void DeleteWishListItem()
        {
            //create wishlist
            var wishList = new WishList();

            //generate a random number of items
            var random = new Random();
            var numberOfItems = random.Next(1,10);

            //Adding items to WishList
            for(var count = 1; count <= numberOfItems; count++){
                wishList.AddItem(count);
            }

            //getting the id to remove
            _testCatalogItemId = random.Next(1, numberOfItems);

            //removing item
            wishList.DeleteItem(_testCatalogItemId);

            Assert.Equal(numberOfItems - 1, wishList.Items.Count);
        }
    }
}