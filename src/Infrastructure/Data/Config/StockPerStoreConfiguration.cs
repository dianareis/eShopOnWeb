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

            builder.HasKey(t => new { t.ItemId, t.StoreId });

            builder.HasOne(c => c.CatalogItem)
                .WithMany(sp => sp.StockPerStore)
                .HasForeignKey(c => c.ItemId);

            builder.HasOne(s => s.Store)
                .WithMany(sp => sp.StockPerStore)
                .HasForeignKey(s => s.StoreId);
        }
    }
}
