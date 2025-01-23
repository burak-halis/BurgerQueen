using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class OrderItemListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } // Öğenin adı (Burger, Drink vb.)
        public string Size { get; set; } // Eğer boyutu varsa
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
