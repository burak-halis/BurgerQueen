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
    public class PromotionMap : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.StartDate)
                .IsRequired();

            builder.Property(p => p.EndDate);

            builder.Property(p => p.DiscountPercentage)
                .IsRequired()
                .HasColumnType("decimal(5,2)"); // Örneğin, %100'ü 100.00 olarak saklamak için

            builder.Property(p => p.IsActive)
                .IsRequired();

            // Burada diğer konfigurasyonlar veya ilişkiler ekleyebilirsiniz
        }
    }
}
