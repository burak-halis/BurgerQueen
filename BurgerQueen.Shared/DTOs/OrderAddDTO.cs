using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class OrderAddDTO
    {
        public string UserId { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderNotes { get; set; }
        public List<OrderItemAddDTO> OrderItems { get; set; } // OrderItems için OrderItemAddDTO kullanılıyor
    }
}
