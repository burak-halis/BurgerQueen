using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class MenusBurgers : BaseEntity
    {
        public int MenuId { get; set; }
        public Menus Menu { get; set; }
        public int BurgerId { get; set; }
        public Burgers Burger { get; set; }
    }
}
