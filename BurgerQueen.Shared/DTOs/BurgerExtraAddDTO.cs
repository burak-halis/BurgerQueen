using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class BurgerExtraAddDTO
    {
        public int BurgerId { get; set; }
        public int ExtraIngredientId { get; set; }
    }
}
