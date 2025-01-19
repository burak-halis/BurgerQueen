using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class Fries : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public int Priority { get; set; } // İsim değişikliği: MenuPriority -> Priority
        public string Sizes { get; set; } // Küçük, Orta, Büyük gibi
        public int? Calories { get; set; }
        public bool IsSpicy { get; set; }
        public string ImagePath { get; set; }
        public string Allergens { get; set; }
        public string Type { get; set; } // Patates türü (örneğin, normal, baharatlı, tatlı patates)
        public virtual ICollection<MenusFries> MenusFries { get; set; }
    }
}
