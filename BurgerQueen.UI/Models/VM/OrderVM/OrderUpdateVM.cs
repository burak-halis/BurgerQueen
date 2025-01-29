using BurgerQueen.Shared.DTOs;
using BurgerQueen.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.OrderVM
{
    public class OrderUpdateVM
    {
        [Key]
        public int Id { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderNotes { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        public OrderUpdateDTO ToOrderUpdateDTO()
        {
            return new OrderUpdateDTO
            {
                Id = this.Id,
                DeliveryAddress = this.DeliveryAddress,
                ExpectedDeliveryDate = this.ExpectedDeliveryDate,
                PaymentMethod = this.PaymentMethod,
                OrderNotes = this.OrderNotes,
                Status = this.Status,
                PaymentStatus = this.PaymentStatus,
                OrderDate = this.OrderDate,
                TotalPrice = this.TotalPrice
            };
        }
    }
}