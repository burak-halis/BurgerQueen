using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int? MenuId { get; set; } // Menünün ID'si
        public virtual Menu Menu { get; set; } // Navigasyon özelliği
        public int? BurgerId { get; set; }
        public virtual Burger Burger { get; set; }
        public int? DrinkId { get; set; }
        public virtual Drink Drink { get; set; }
        public int? FriesId { get; set; }
        public virtual Fry Fries { get; set; }
        public int? SideItemId { get; set; }
        public virtual SideItem SideItem { get; set; }
        public int? SauceId { get; set; }
        public virtual Sauce Sauce { get; set; }
        public ICollection<BurgerExtra> Extras { get; set; }
        public string BurgerSize { get; set; } // Seçilen burger boyutu
        public string DrinkSize { get; set; }
        public string FriesSize { get; set; }
        public int Quantity { get; set; } // Ek: Miktar
        public string Customizations { get; set; } // Ek: Özel talepler
        public decimal Price { get; set; }
    }
}
