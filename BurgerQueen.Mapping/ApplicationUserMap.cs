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
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(au => au.FirstName)
                .HasMaxLength(50);

            builder.Property(au => au.LastName)
                .HasMaxLength(50);

            builder.Property(au => au.DateOfBirth);

            builder.Property(au => au.Address)
                .HasMaxLength(255);

            builder.Property(au => au.City)
                .HasMaxLength(100);

            builder.Property(au => au.Country)
                .HasMaxLength(100);

            builder.Property(au => au.ProfilePictureUrl)
                .HasMaxLength(255);

            builder.Property(au => au.PreferredLanguage)
                .HasMaxLength(2);

            builder.Property(au => au.LastLoginDate);

            builder.Property(au => au.CreatedDate)
                .IsRequired();

            builder.Property(au => au.ModifiedDate);

            builder.Property(au => au.IsActive)
                .IsRequired();

            // İlişkiler (örneğin, siparişler, favoriler)
            builder.HasMany(au => au.Orders)
                   .WithOne(o => o.User)
                   .HasForeignKey(o => o.UserId);

            builder.HasMany(au => au.FavoriteBurgers)
                   .WithOne(fb => fb.User)
                   .HasForeignKey(fb => fb.UserId);

            builder.HasMany(au => au.FavoriteDrinks)
                   .WithOne(fd => fd.User)
                   .HasForeignKey(fd => fd.UserId);

            builder.HasMany(au => au.FavoriteFries)
                   .WithOne(ff => ff.User)
                   .HasForeignKey(ff => ff.UserId);

            builder.HasMany(au => au.FavoriteSideItems)
                   .WithOne(fsi => fsi.User)
                   .HasForeignKey(fsi => fsi.UserId);

            builder.HasMany(au => au.FavoriteSauces)
                   .WithOne(fs => fs.User)
                   .HasForeignKey(fs => fs.UserId);

            builder.HasMany(au => au.FavoriteMenus)
                   .WithOne(fm => fm.User)
                   .HasForeignKey(fm => fm.UserId);
        }
    }
}
