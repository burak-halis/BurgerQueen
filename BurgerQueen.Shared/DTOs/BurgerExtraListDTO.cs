using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class BurgerExtraListDTO
    {
        public int Id { get; set; }
        public int BurgerId { get; set; }
        public int ExtraIngredientId { get; set; }
    }
}
