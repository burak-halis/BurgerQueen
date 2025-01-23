using BurgerQueen.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; } // Enum olarak güncellendi
        public string DeliveryAddress { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string OrderConfirmationNumber { get; set; }
        public string PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; } // Enum olarak güncellendi
        public string OrderNotes { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? ShippingFee { get; set; }
        public virtual ICollection<OrderItem> OrdeItems { get; set; }
        public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }

    }
}
