using BurgerQueen.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.OrderVM
{
    public class OrderAddVM
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string DeliveryAddress { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ExpectedDeliveryDate { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }

        [StringLength(1000)]
        public string OrderNotes { get; set; }

        [Required]
        public List<OrderItemAddVM> OrderItems { get; set; }

        public OrderAddDTO ToOrderAddDTO()
        {
            return new OrderAddDTO
            {
                UserId = this.UserId,
                DeliveryAddress = this.DeliveryAddress,
                ExpectedDeliveryDate = this.ExpectedDeliveryDate,
                PaymentMethod = this.PaymentMethod,
                OrderNotes = this.OrderNotes,
                OrderItems = this.OrderItems?.Select(oi => oi.ToOrderItemAddDTO()).ToList()
            };
        }
    }
}