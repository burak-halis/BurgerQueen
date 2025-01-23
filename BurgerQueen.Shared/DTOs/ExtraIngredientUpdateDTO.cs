using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class ExtraIngredientUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsVegan { get; set; }
        public string ImagePath { get; set; }
        public string UnitOfMeasure { get; set; }
    }
}
