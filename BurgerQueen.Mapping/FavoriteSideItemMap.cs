using BurgerQueen.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Mapping
{
    public class FavoriteSideItemMap : IEntityTypeConfiguration<FavoriteSideItem>
    {
        public void Configure(EntityTypeBuilder<FavoriteSideItem> builder)
        {
            builder.HasKey(fsi => new { fsi.UserId, fsi.SideItemId });

            builder.HasOne(fsi => fsi.User)
                .WithMany(u => u.FavoriteSideItems)
                .HasForeignKey(fsi => fsi.UserId);

            builder.HasOne(fsi => fsi.SideItem)
                .WithMany()
                .HasForeignKey(fsi => fsi.SideItemId);
        }
    }
}
