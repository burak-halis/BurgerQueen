using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class OrderItems : BaseEntity
    {
        public int OrderId { get; set; }
        public virtual Orders Order { get; set; }
        public int? MenuId { get; set; } // Menünün ID'si
        public virtual Menus Menu { get; set; } // Navigasyon özelliği
        public int? BurgerId { get; set; }
        public virtual Burgers Burger { get; set; }
        public int? DrinkId { get; set; }
        public virtual Drinks Drink { get; set; }
        public int? FriesId { get; set; }
        public virtual Fries Fries { get; set; }
        public int? SideItemId { get; set; }
        public virtual SideItems SideItem { get; set; }
        public int? SauceId { get; set; }
        public virtual Sauces Sauce { get; set; }
        public ICollection<BurgersExtras> Extras { get; set; }
        public string BurgerSize { get; set; } // Seçilen burger boyutu
        public string DrinkSize { get; set; }
        public string FriesSize { get; set; }
        public int Quantity { get; set; } // Ek: Miktar
        public string Customizations { get; set; } // Ek: Özel talepler
        public decimal Price { get; set; }
    }
}
