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
    public class MenuMap : BaseMap<Menu>
    {
        public override void Configure(EntityTypeBuilder<Menu> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Description)
                .HasMaxLength(500);

            builder.Property(m => m.TotalPrice)
                .HasColumnType("decimal(18, 2)");

            builder.Property(m => m.ImagePath)
                .HasMaxLength(255);

            builder.Property(m => m.Popularity);

            builder.Property(m => m.Discount)
                .HasColumnType("decimal(18, 2)");

            builder.Property(m => m.MenuType)
                .HasMaxLength(50);

            // Navigasyon özellikleri için ilişkiler
            builder.HasMany(m => m.MenuBurgers)
                   .WithOne(mb => mb.Menu)
                   .HasForeignKey(mb => mb.MenuId);

            builder.HasMany(m => m.MenuDrinks)
                   .WithOne(md => md.Menu)
                   .HasForeignKey(md => md.MenuId);

            builder.HasMany(m => m.MenuFries)
                   .WithOne(mf => mf.Menu)
                   .HasForeignKey(mf => mf.MenuId);

            builder.HasMany(m => m.MenuSideItems)
                   .WithOne(ms => ms.Menu)
                   .HasForeignKey(ms => ms.MenuId);
        }
    }
}
