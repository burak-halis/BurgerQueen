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
    public class FavoriteBurgerMap : IEntityTypeConfiguration<FavoriteBurger>
    {
        public void Configure(EntityTypeBuilder<FavoriteBurger> builder)
        {
            builder.HasKey(fb => new { fb.UserId, fb.BurgerId });

            builder.HasOne(fb => fb.User)
                .WithMany(u => u.FavoriteBurgers)
                .HasForeignKey(fb => fb.UserId);

            builder.HasOne(fb => fb.Burger)
                .WithMany()
                .HasForeignKey(fb => fb.BurgerId);
        }
    }
}
