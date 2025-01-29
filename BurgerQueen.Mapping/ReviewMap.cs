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
    public class ReviewMap : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Content)
                .IsRequired();

            builder.Property(r => r.Rating)
                .IsRequired();

            // Ürün türlerine ilişkilendirmeler
            builder.HasOne(r => r.Burger)
                .WithMany()
                .HasForeignKey(r => r.BurgerId)
                .IsRequired(false);

            builder.HasOne(r => r.Drink)
                .WithMany()
                .HasForeignKey(r => r.DrinkId)
                .IsRequired(false);

            builder.HasOne(r => r.Fry)
                .WithMany()
                .HasForeignKey(r => r.FryId)
                .IsRequired(false);

            builder.HasOne(r => r.SideItem)
                .WithMany()
                .HasForeignKey(r => r.SideItemId)
                .IsRequired(false);

            builder.HasOne(r => r.Sauce)
                .WithMany()
                .HasForeignKey(r => r.SauceId)
                .IsRequired(false);

            builder.HasOne(r => r.Menu)
                .WithMany()
                .HasForeignKey(r => r.MenuId)
                .IsRequired(false);

            // Kullanıcı ilişkilendirmesi
            builder.HasOne(r => r.User)
                .WithMany() // Kullanıcının birden fazla yorumu olabilir
                .HasForeignKey(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.CreatedDate)
                .IsRequired();

            builder.Property(r => r.ModifiedDate);
        }
    }
}
