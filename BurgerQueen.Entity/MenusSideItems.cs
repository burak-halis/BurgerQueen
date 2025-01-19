using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class MenusSideItems : BaseEntity
    {
        public int MenuId { get; set; }
        public Menus Menu { get; set; }
        public int SideItemId { get; set; }
        public SideItems SideItem { get; set; }
    }
}
