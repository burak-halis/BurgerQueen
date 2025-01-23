using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class Burger : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public int Priority { get; set; } // İsim değişikliği: MenuPriority -> Priority
        public string ImagePath { get; set; } // Burgerin resminin yolu
        public int? Calories { get; set; } // Kalori bilgisi
        public bool IsVegetarian { get; set; } // Vejetaryen mi?
        public bool IsVegan { get; set; } // Vegan mı?
        public bool IsGlutenFree { get; set; } // Glutensiz mi?
        public string Ingredients { get; set; } // Ana bileşenler
        public int? Popularity { get; set; } // Örneğin, 1-5 arası yıldız puanlaması
        public DateTime? ReleaseDate { get; set; } // Çıkış veya sezonluk satış tarihi
        public TimeSpan? PreparationTime { get; set; }
        public string Allergens { get; set; } // Alerjen maddeler
        public string Sizes { get; set; } // Küçük, Orta, Büyük gibi
        public virtual ICollection<MenuBurger> MenuBurgers { get; set; }
        public virtual ICollection<BurgerExtra> BurgerExtras { get; set; }
    }
}
