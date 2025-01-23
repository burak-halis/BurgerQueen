using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class FavoriteSauceListDTO
    {
        public string UserId { get; set; }
        public int SauceId { get; set; }
        public string SauceName { get; set; } // Sauce'un adını listelemek için
        public DateTime AddedDate { get; set; }
        public string Notes { get; set; }
    }
}
