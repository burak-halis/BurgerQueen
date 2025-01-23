using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class MenuSideItem : BaseEntity
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int SideItemId { get; set; }
        public SideItem SideItem { get; set; }
    }
}
