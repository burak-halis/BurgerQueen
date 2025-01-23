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
    public class ExtraIngredientMap : BaseMap<ExtraIngredient>
    {
        public override void Configure(EntityTypeBuilder<ExtraIngredient> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasMaxLength(500);

            builder.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.Discount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.Priority)
                .IsRequired();

            builder.Property(e => e.Category)
                .HasMaxLength(50);

            builder.Property(e => e.IsGlutenFree)
                .HasDefaultValue(false);

            builder.Property(e => e.IsVegan)
                .HasDefaultValue(false);

            builder.Property(e => e.Allergens)
                .HasMaxLength(500);

            builder.Property(e => e.ImagePath)
                .HasMaxLength(255);

            builder.Property(e => e.UnitOfMeasure)
                .HasMaxLength(20);

            // Navigasyon özellikleri için ilişkiler
            builder.HasMany(e => e.BurgerExtras)
                   .WithOne(be => be.ExtraIngredient)
                   .HasForeignKey(be => be.ExtraIngredientId);
        }
    }
}
