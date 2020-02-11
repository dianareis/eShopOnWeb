using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config
{
    public class StockPerStoreConfiguration : IEntityTypeConfiguration<StockPerStore>
    {
        public void Configure(EntityTypeBuilder<StockPerStore> builder)
        {
            builder.ToTable("StockPerStore");

            builder.HasKey(StockPerStore => new { StockPerStore.ItemId, StockPerStore.StoreId });

            builder.HasOne(StockPerStore => StockPerStore.CatalogItem)
                .WithMany(CatalogItem => CatalogItem.StockPerStore)
                .HasForeignKey(StockPerStore => StockPerStore.ItemId);

            builder.HasOne(StockPerStore => StockPerStore.Store)
                .WithMany(Store => Store.StockPerStore)
                .HasForeignKey(StockPerStore => StockPerStore.StoreId);
        }
    }
}
