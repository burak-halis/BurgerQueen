using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class BurgerExtra : BaseEntity
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int BurgerId { get; set; }
        public Burger Burger { get; set; }
        public int ExtraIngredientId { get; set; }
        public ExtraIngredient ExtraIngredient { get; set; }
        public int OrderItemId { get; set; } // Ekledik
        public OrderItem OrderItem { get; set; } // Ekledik
    }
}
