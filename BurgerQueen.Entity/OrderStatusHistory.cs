using BurgerQueen.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class OrderStatusHistory : BaseEntity
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public OrderStatus Status { get; set; } // Enum olarak güncellendi
        public DateTime ChangeTimestamp { get; set; }
    }
}
