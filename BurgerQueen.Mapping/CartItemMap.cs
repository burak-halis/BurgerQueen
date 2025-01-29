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
    public class CartItemMap : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.ProductId).IsRequired();
            builder.Property(c => c.ProductType).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Quantity).IsRequired();
        }
    }
}
