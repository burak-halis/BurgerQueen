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
    public class SideItemMap : BaseMap<SideItem>
    {
        public override void Configure(EntityTypeBuilder<SideItem> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Description)
                .HasMaxLength(500);

            builder.Property(s => s.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Property(s => s.Discount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(s => s.Calories);

            builder.Property(s => s.ImagePath)
                .HasMaxLength(255);

            builder.Property(s => s.IsGlutenFree)
                .HasDefaultValue(false);

            builder.Property(s => s.IsVegetarian)
                .HasDefaultValue(false);

            builder.Property(s => s.IsVegan)
                .HasDefaultValue(false);

            builder.Property(s => s.Allergens)
                .HasMaxLength(500);

            builder.Property(s => s.Priority)
                .IsRequired();

            // Navigasyon özellikleri için ilişkiler
            builder.HasMany(s => s.MenuSideItems)
                   .WithOne(ms => ms.SideItem)
                   .HasForeignKey(ms => ms.SideItemId);
        }
    }
}
