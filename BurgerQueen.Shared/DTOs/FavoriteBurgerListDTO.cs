using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class FavoriteBurgerListDTO
    {
        public string UserId { get; set; }
        public int BurgerId { get; set; }
        public string BurgerName { get; set; } // Burger'in adını listelemek için
        public DateTime AddedDate { get; set; }
        public string Notes { get; set; }
    }
}
