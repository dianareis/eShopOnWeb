using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Store");

            builder.HasMany(Store => Store.StockPerStore)
                .WithOne(StockPerStore => StockPerStore.Store)
                .HasForeignKey(StockPerStore => StockPerStore.StoreId);
        }
    }
}
