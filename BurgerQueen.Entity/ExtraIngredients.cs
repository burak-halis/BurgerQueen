using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class ExtraIngredients : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public int Priority { get; set; } // İsim değişikliği: MenuPriority -> Priority
        public string Category { get; set; }
        public bool IsGlutenFree { get; set; }
        public bool IsVegan { get; set; }
        public string Allergens { get; set; }
        public string ImagePath { get; set; }
        public string UnitOfMeasure { get; set; }
        public virtual ICollection<BurgersExtras> BurgersExtras { get; set; }
    }
}
