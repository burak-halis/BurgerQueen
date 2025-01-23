using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class FavoriteDrinkListDTO
    {
        public string UserId { get; set; }
        public int DrinkId { get; set; }
        public string DrinkName { get; set; } // Drink'in adını listelemek için
        public DateTime AddedDate { get; set; }
        public string Notes { get; set; }
    }
}
