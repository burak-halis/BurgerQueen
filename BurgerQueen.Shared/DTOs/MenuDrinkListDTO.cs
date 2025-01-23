using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class MenuDrinkListDTO
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int DrinkId { get; set; }
    }
}
