using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class MenuDrink : BaseEntity
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
    }
}
