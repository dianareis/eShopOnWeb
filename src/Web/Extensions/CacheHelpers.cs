using System;

namespace Microsoft.eShopWeb.Web.Extensions
{
    public static class CacheHelpers
    {
        public static readonly TimeSpan DefaultCacheDuration = TimeSpan.FromSeconds(30);
        private static readonly string _itemsKeyTemplate = "items-{0}-{1}-{2}-{3}-{4}";

        public static string GenerateCatalogItemCacheKey(int pageIndex, int itemsPage, string searchText, int? brandId, int? typeId)
        {
            if(pageIndex < 0) {
                throw new InvalidPageIndexException();
            }
            string _searchText = string.IsNullOrEmpty(searchText)?"":searchText.Replace(" ", ""); // TODO: Handle invalid special chars in cache keys?
            return string.Format(_itemsKeyTemplate, pageIndex, itemsPage, _searchText, brandId, typeId);
        }

        public static string GenerateBrandsCacheKey()
        {
            return "brands";
        }

        public static string GenerateTypesCacheKey()
        {
            return "types";
        }

        public static string GenerateCatalogItemIdKey(int id)
        {
            return $"catalog_item_{id}";
        }
    }

    public class InvalidPageIndexException : Exception
    {
        
    }
}
