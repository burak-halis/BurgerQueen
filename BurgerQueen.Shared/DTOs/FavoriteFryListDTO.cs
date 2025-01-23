using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class FavoriteFryListDTO
    {
        public string UserId { get; set; }
        public int FriesId { get; set; }
        public string FriesName { get; set; } // Fries'in adını listelemek için
        public DateTime AddedDate { get; set; }
        public string Notes { get; set; }
    }
}
