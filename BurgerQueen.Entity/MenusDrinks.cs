using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class MenusDrinks : BaseEntity
    {
        public int MenuId { get; set; }
        public Menus Menu { get; set; }
        public int DrinkId { get; set; }
        public Drinks Drink { get; set; }
    }
}
