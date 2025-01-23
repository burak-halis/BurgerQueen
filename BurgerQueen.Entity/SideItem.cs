using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class SideItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public int? Calories { get; set; }
        public string ImagePath { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsVegetarian { get; set; }
        public string Allergens { get; set; } // Alerjen maddeler
        public bool IsVegan { get; set; } // Vegan mı?
        public int Priority { get; set; } // İsim değişikliği: MenuPriority -> Priority
        public virtual ICollection<MenuSideItem> MenuSideItems { get; set; }
    }
}
