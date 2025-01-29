using BurgerQueen.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.OrderVM
{
    public class OrderListVM
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
    }
}