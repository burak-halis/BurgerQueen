using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class BurgersExtras : BaseEntity
    {
        public int BurgerId { get; set; }
        public Burgers Burger { get; set; }
        public int ExtraIngredientId { get; set; }
        public ExtraIngredients ExtraIngredient { get; set; }
    }
}
