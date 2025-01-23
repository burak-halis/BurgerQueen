using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class MenuBurger : BaseEntity
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int BurgerId { get; set; }
        public Burger Burger { get; set; }
    }
}
