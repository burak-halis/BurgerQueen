using BurgerQueen.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class OrderStatusHistoryAddDTO
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
