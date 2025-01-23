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
    public class FavoriteSauceMap : IEntityTypeConfiguration<FavoriteSauce>
    {
        public void Configure(EntityTypeBuilder<FavoriteSauce> builder)
        {
            builder.HasKey(fs => new { fs.UserId, fs.SauceId });

            builder.HasOne(fs => fs.User)
                .WithMany(u => u.FavoriteSauces)
                .HasForeignKey(fs => fs.UserId);

            builder.HasOne(fs => fs.Sauce)
                .WithMany()
                .HasForeignKey(fs => fs.SauceId);
        }
    }
}
