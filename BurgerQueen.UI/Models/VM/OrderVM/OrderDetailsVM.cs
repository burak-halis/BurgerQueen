using BurgerQueen.Shared.DTOs;

namespace BurgerQueen.UI.Models.VM.OrderVM
{
    public class OrderDetailsVM
    {
        public OrderUpdateVM Order { get; set; }
        public IEnumerable<OrderItemListDTO> OrderItems { get; set; }
    }
}