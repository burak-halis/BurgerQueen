using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class Menus : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? TotalPrice { get; set; }
        public string ImagePath { get; set; }
        public int? Popularity { get; set; } // Örneğin, 1-5 arası yıldız puanlaması
        public decimal? Discount { get; set; } // Örneğin, %10 indirim için 0.10
        public string MenuType { get; set; }

        public virtual ICollection<MenusBurgers> MenusBurgers { get; set; }
        public virtual ICollection<MenusDrinks> MenusDrinks { get; set; }
        public virtual ICollection<MenusFries> MenusFries { get; set; }
        public virtual ICollection<MenusSideItems> MenusSideItems { get; set; }

    }
}
