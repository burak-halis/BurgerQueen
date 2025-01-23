using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? TotalPrice { get; set; }
        public string ImagePath { get; set; }
        public int? Popularity { get; set; } // Örneğin, 1-5 arası yıldız puanlaması
        public decimal? Discount { get; set; } // Örneğin, %10 indirim için 0.10
        public string MenuType { get; set; }

        public virtual ICollection<MenuBurger> MenuBurgers { get; set; }
        public virtual ICollection<MenuDrink> MenuDrinks { get; set; }
        public virtual ICollection<MenuFry> MenuFries { get; set; }
        public virtual ICollection<MenuSideItem> MenuSideItems { get; set; }

    }
}
