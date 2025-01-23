using BurgerQueen.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Mapping
{
    public class MenuBurgerMap : BaseMap<MenuBurger>
    {
        public override void Configure(EntityTypeBuilder<MenuBurger> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.HasKey(mb => new { mb.MenuId, mb.BurgerId }); // Composite key

            builder.Property(mb => mb.MenuId)
                .IsRequired();

            builder.Property(mb => mb.BurgerId)
                .IsRequired();

            // İlişkiler
            builder.HasOne(mb => mb.Menu)
                   .WithMany(m => m.MenuBurgers)
                   .HasForeignKey(mb => mb.MenuId);

            builder.HasOne(mb => mb.Burger)
                   .WithMany(b => b.MenuBurgers)
                   .HasForeignKey(mb => mb.BurgerId);
        }
    }
}
