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
    public class FavoriteMenuMap : IEntityTypeConfiguration<FavoriteMenu>
    {
        public void Configure(EntityTypeBuilder<FavoriteMenu> builder)
        {
            builder.HasKey(fm => new { fm.UserId, fm.MenuId });

            builder.HasOne(fm => fm.User)
                .WithMany(u => u.FavoriteMenus)
                .HasForeignKey(fm => fm.UserId);

            builder.HasOne(fm => fm.Menu)
                .WithMany()
                .HasForeignKey(fm => fm.MenuId);
        }
    }
}
