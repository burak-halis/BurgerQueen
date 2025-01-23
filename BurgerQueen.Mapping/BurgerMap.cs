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
    public class BurgerMap : BaseMap<Burger>
    {
        public override void Configure(EntityTypeBuilder<Burger> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.Description)
                .HasMaxLength(500);

            builder.Property(b => b.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Property(b => b.Discount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(b => b.Priority)
                .IsRequired();

            builder.Property(b => b.ImagePath)
                .HasMaxLength(255);

            builder.Property(b => b.Calories);

            builder.Property(b => b.IsVegetarian)
                .HasDefaultValue(false);

            builder.Property(b => b.IsVegan)
                .HasDefaultValue(false);

            builder.Property(b => b.IsGlutenFree)
                .HasDefaultValue(false);

            builder.Property(b => b.Ingredients)
                .HasMaxLength(1000);

            builder.Property(b => b.Popularity);

            builder.Property(b => b.ReleaseDate);

            builder.Property(b => b.PreparationTime);

            builder.Property(b => b.Allergens)
                .HasMaxLength(500);

            builder.Property(b => b.Sizes)
                .HasMaxLength(255);

            // Navigasyon özellikleri için ilişkiler
            builder.HasMany(b => b.MenuBurgers)
                   .WithOne(mb => mb.Burger)
                   .HasForeignKey(mb => mb.BurgerId);

            builder.HasMany(b => b.BurgerExtras)
                   .WithOne(be => be.Burger)
                   .HasForeignKey(be => be.BurgerId);
        }
    }
}
