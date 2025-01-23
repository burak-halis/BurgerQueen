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
    public class FavoriteFryMap : IEntityTypeConfiguration<FavoriteFry>
    {
        public void Configure(EntityTypeBuilder<FavoriteFry> builder)
        {
            builder.HasKey(ff => new { ff.UserId, ff.FriesId });

            builder.HasOne(ff => ff.User)
                .WithMany(u => u.FavoriteFries)
                .HasForeignKey(ff => ff.UserId);

            builder.HasOne(ff => ff.Fries)
                .WithMany()
                .HasForeignKey(ff => ff.FriesId);
        }
    }
}
