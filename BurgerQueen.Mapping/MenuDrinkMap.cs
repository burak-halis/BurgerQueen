using BurgerQueen.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Mapping
{
    public class MenuDrinkMap : BaseMap<MenuDrink>
    {
        public override void Configure(EntityTypeBuilder<MenuDrink> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.HasKey(md => new { md.MenuId, md.DrinkId }); // Composite key

            builder.Property(md => md.MenuId)
                .IsRequired();

            builder.Property(md => md.DrinkId)
                .IsRequired();

            // İlişkiler
            builder.HasOne(md => md.Menu)
                   .WithMany(m => m.MenuDrinks)
                   .HasForeignKey(md => md.MenuId);

            builder.HasOne(md => md.Drink)
                   .WithMany(d => d.MenuDrinks)
                   .HasForeignKey(md => md.DrinkId);
        }
    }
}
