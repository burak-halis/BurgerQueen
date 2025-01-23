using BurgerQueen.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Mapping
{
    public class BurgerExtraMap : BaseMap<BurgerExtra>
    {
        public override void Configure(EntityTypeBuilder<BurgerExtra> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.HasKey(be => new { be.BurgerId, be.ExtraIngredientId }); // Composite key

            builder.Property(be => be.BurgerId)
                .IsRequired();

            builder.Property(be => be.ExtraIngredientId)
                .IsRequired();

            // İlişkiler
            builder.HasOne(be => be.Burger)
                   .WithMany(b => b.BurgerExtras)
                   .HasForeignKey(be => be.BurgerId);

            builder.HasOne(be => be.ExtraIngredient)
                   .WithMany(ei => ei.BurgerExtras)
                   .HasForeignKey(be => be.ExtraIngredientId);
        }
    }
}
