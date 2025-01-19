using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class MenusFries : BaseEntity
    {
        public int MenuId { get; set; }
        public Menus Menu { get; set; }
        public int FriesId { get; set; }
        public Fries Fries { get; set; }
    }
}
