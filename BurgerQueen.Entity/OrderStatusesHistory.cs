using BurgerQueen.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class OrderStatusesHistory : BaseEntity
    {
        public int OrderId { get; set; }
        public virtual Orders Order { get; set; }
        public OrderStatus Status { get; set; } // Enum olarak güncellendi
        public DateTime ChangeTimestamp { get; set; }
    }
}
