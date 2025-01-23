using BurgerQueen.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class OrderStatusHistoryListDTO
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime ChangeTimestamp { get; set; }
    }
}
