using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class Drink : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string ImagePath { get; set; }
        public int Priority { get; set; } // İsim değişikliği: MenuPriority -> Priority
        public string Sizes { get; set; } // Küçük, Orta, Büyük gibi
        public int? Calories { get; set; }
        public virtual ICollection<MenuDrink> MenuDrinks { get; set; }

    }
}
