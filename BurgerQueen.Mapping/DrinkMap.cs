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
    public class DrinkMap : BaseMap<Drink>
    {
        public override void Configure(EntityTypeBuilder<Drink> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.Property(d => d.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Property(d => d.Discount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(d => d.Priority)
                .IsRequired();

            builder.Property(d => d.ImagePath)
                .HasMaxLength(255);

            builder.Property(d => d.Sizes)
                .HasMaxLength(255);

            builder.Property(d => d.Calories);

            // Navigasyon özellikleri için ilişkiler
            builder.HasMany(d => d.MenuDrinks)
                   .WithOne(md => md.Drink)
                   .HasForeignKey(md => md.DrinkId);
        }
    }
}
