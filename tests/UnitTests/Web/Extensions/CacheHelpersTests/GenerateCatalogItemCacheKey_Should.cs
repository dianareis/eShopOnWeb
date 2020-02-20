using System;
using Microsoft.eShopWeb.Web;
using Microsoft.eShopWeb.Web.Extensions;
using Xunit;

namespace Microsoft.eShopWeb.UnitTests.Web.Extensions.CacheHelpersTests
{
    public class GenerateCatalogItemCacheKey_Should
    {
        [Theory]
        [InlineData(0, Constants.ITEMS_PER_PAGE, null, null, null, "en-US", "items-0-10----en-US")]
        [InlineData(5, 20, null, null, null, "pt-PT", "items-5-20----pt-PT")]
        [InlineData(-5, 20, null, null, null, "en-US", null, typeof(InvalidPageIndexException))]
        public void ReturnCatalogItemCacheKey(int pageIndex, int itemPerPage, string searchText, int? brandId, int? typeId,  string cultureName, string expectedResult, Type exceptionType = null)
        {
            if(string.IsNullOrEmpty(expectedResult)) {
                if(exceptionType == null) {
                    throw new Exception("Missing exception type to check");
                }
                Assert.Throws(exceptionType,
                () => CacheHelpers.GenerateCatalogItemCacheKey(pageIndex, itemPerPage, searchText, brandId, typeId, cultureName));
            } else {
                var result = CacheHelpers.GenerateCatalogItemCacheKey(pageIndex, itemPerPage, searchText, brandId, typeId, cultureName);

                Assert.Equal(expectedResult, result);
            }
        }

        // [Fact]
        // public void ReturnCatalogItemCacheKey()
        // {
        //     var pageIndex = 0;
        //     int? brandId = null;
        //     int? typeId = null;

        //     var result = CacheHelpers.GenerateCatalogItemCacheKey(pageIndex, Constants.ITEMS_PER_PAGE, "", brandId, typeId);

        //     Assert.Equal("items-0-10---", result);
        // }

    }
}
