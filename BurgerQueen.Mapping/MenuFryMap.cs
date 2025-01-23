using BurgerQueen.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Mapping
{
    public class MenuFryMap : BaseMap<MenuFry>
    {
        public override void Configure(EntityTypeBuilder<MenuFry> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.HasKey(mf => new { mf.MenuId, mf.FryId }); // Composite key

            builder.Property(mf => mf.MenuId)
                .IsRequired();

            builder.Property(mf => mf.FryId)
                .IsRequired();

            // İlişkiler
            builder.HasOne(mf => mf.Menu)
                   .WithMany(m => m.MenuFries)
                   .HasForeignKey(mf => mf.MenuId);

            builder.HasOne(mf => mf.Fry)
                   .WithMany(f => f.MenuFries)
                   .HasForeignKey(mf => mf.FryId);
        }
    }
}

