using BurgerQueen.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Mapping
{
    public class OrderMap : BaseMap<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.Property(o => o.UserId)
                .IsRequired()
                .HasMaxLength(450); // Identity'nin varsayılan UserId uzunluğu

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(o => o.Status)
                .HasConversion<string>();

            builder.Property(o => o.DeliveryAddress)
                .HasMaxLength(500);

            builder.Property(o => o.ExpectedDeliveryDate);

            builder.Property(o => o.OrderConfirmationNumber)
                .HasMaxLength(50);

            builder.Property(o => o.PaymentMethod)
                .HasMaxLength(50);

            builder.Property(o => o.PaymentStatus)
                .HasConversion<string>();

            builder.Property(o => o.OrderNotes)
                .HasMaxLength(1000);

            builder.Property(o => o.TotalPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(o => o.ShippingFee)
                .HasColumnType("decimal(18, 2)");

            // İlişkiler
            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId);

            builder.HasMany(o => o.OrdeItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId);
        }
    }
}
