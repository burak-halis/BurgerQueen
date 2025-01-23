using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class DrinkAddDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Calories { get; set; }
        public string Sizes { get; set; } // Küçük, Orta, Büyük gibi
        public bool IsSugary { get; set; }
        public bool IsAlcoholic { get; set; }
        public string Ingredients { get; set; }
        public string ImagePath { get; set; }
    }
}
