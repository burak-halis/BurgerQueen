using BurgerQueen.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class OrderUpdateDTO
    {
        public int Id { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderNotes { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; } // Eklendi
    }
}
