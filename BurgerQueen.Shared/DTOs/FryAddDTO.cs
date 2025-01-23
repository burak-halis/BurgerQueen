using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class FryAddDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? Calories { get; set; }
        public string Sizes { get; set; }
        public bool IsSpicy { get; set; }
        public string ImagePath { get; set; }
        public string Type { get; set; } // Patates türü (örneğin, normal, baharatlı, tatlı patates)
    }
}
