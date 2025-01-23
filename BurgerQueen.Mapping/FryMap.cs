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
    public class FryMap : BaseMap<Fry>
    {
        public override void Configure(EntityTypeBuilder<Fry> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.Description)
                .HasMaxLength(500);

            builder.Property(f => f.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Property(f => f.Discount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(f => f.Priority)
                .IsRequired();

            builder.Property(f => f.Sizes)
                .HasMaxLength(255);

            builder.Property(f => f.Calories);

            builder.Property(f => f.IsSpicy)
                .HasDefaultValue(false);

            builder.Property(f => f.ImagePath)
                .HasMaxLength(255);

            builder.Property(f => f.Allergens)
                .HasMaxLength(500);

            builder.Property(f => f.Type)
                .HasMaxLength(50);

            // Navigasyon özellikleri için ilişkiler
            builder.HasMany(f => f.MenuFries)
                   .WithOne(mf => mf.Fry)
                   .HasForeignKey(mf => mf.FryId);
        }
    }
}
