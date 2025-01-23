using BurgerQueen.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Mapping
{
    public class MenuSideItemMap : BaseMap<MenuSideItem>
    {
        public override void Configure(EntityTypeBuilder<MenuSideItem> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.HasKey(ms => new { ms.MenuId, ms.SideItemId }); // Composite key

            builder.Property(ms => ms.MenuId)
                .IsRequired();

            builder.Property(ms => ms.SideItemId)
                .IsRequired();

            // İlişkiler
            builder.HasOne(ms => ms.Menu)
                   .WithMany(m => m.MenuSideItems)
                   .HasForeignKey(ms => ms.MenuId);

            builder.HasOne(ms => ms.SideItem)
                   .WithMany(si => si.MenuSideItems)
                   .HasForeignKey(ms => ms.SideItemId);
        }
    }
}
