﻿using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using System.Reflection;

namespace Microsoft.eShopWeb.Infrastructure.Data
{

    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<StockPerStore> StockPerStore { get; set; }
        public DbSet<Order> Orders { get; set; }        
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<WishListItem> WishListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* adiciona propriedade para pré-definir o valor do showpirce para true
            builder.Entity<CatalogItem>()
                .Property(x => x.ShowPrice)
                .HasDefaultValue(true); */

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
