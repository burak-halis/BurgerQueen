using BurgerQueen.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Mapping
{
    public class OrderStatusHistoryMap : BaseMap<OrderStatusHistory>
    {
        public override void Configure(EntityTypeBuilder<OrderStatusHistory> builder)
        {
            base.Configure(builder); // BaseMap'teki temel konfigurasyonları uyguluyoruz

            builder.Property(osh => osh.OrderId)
                .IsRequired();

            builder.Property(osh => osh.Status)
                .HasConversion<string>() // Enum değerini string'e çevirir
                .IsRequired();

            builder.Property(osh => osh.ChangeTimestamp)
                .IsRequired();

            // İlişki
            builder.HasOne(osh => osh.Order)
                   .WithMany(o => o.OrderStatusHistories) // Order sınıfında bu koleksiyonun olması gerekir
                   .HasForeignKey(osh => osh.OrderId);
        }
    }
}
