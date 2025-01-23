using BurgerQueen.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Mapping
{
    public class OrderItemMap : BaseMap<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.Property(oi => oi.OrderId)
                .IsRequired();

            builder.Property(oi => oi.MenuId);

            builder.Property(oi => oi.BurgerId);

            builder.Property(oi => oi.DrinkId);

            builder.Property(oi => oi.FriesId);

            builder.Property(oi => oi.SideItemId);

            builder.Property(oi => oi.SauceId);

            builder.Property(oi => oi.BurgerSize)
                .HasMaxLength(50);

            builder.Property(oi => oi.DrinkSize)
                .HasMaxLength(50);

            builder.Property(oi => oi.FriesSize)
                .HasMaxLength(50);

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.Property(oi => oi.Customizations)
                .HasMaxLength(1000);

            builder.Property(oi => oi.Price)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            // İlişkiler
            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrdeItems)
                   .HasForeignKey(oi => oi.OrderId);

            builder.HasOne(oi => oi.Menu)
                   .WithMany()
                   .HasForeignKey(oi => oi.MenuId);

            builder.HasOne(oi => oi.Burger)
                   .WithMany()
                   .HasForeignKey(oi => oi.BurgerId);

            builder.HasOne(oi => oi.Drink)
                   .WithMany()
                   .HasForeignKey(oi => oi.DrinkId);

            builder.HasOne(oi => oi.Fries)
                   .WithMany()
                   .HasForeignKey(oi => oi.FriesId);

            builder.HasOne(oi => oi.SideItem)
                   .WithMany()
                   .HasForeignKey(oi => oi.SideItemId);

            builder.HasOne(oi => oi.Sauce)
                   .WithMany()
                   .HasForeignKey(oi => oi.SauceId);

            builder.HasMany(oi => oi.Extras)
                   .WithOne(be => be.OrderItem)
                   .HasForeignKey(be => be.OrderItemId);
        }
    }
}

