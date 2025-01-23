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
    public class FavoriteDrinkMap : IEntityTypeConfiguration<FavoriteDrink>
    {
        public void Configure(EntityTypeBuilder<FavoriteDrink> builder)
        {
            builder.HasKey(fd => new { fd.UserId, fd.DrinkId });

            builder.HasOne(fd => fd.User)
                .WithMany(u => u.FavoriteDrinks)
                .HasForeignKey(fd => fd.UserId);

            builder.HasOne(fd => fd.Drink)
                .WithMany()
                .HasForeignKey(fd => fd.DrinkId);
        }
    }
}
