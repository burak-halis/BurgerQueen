using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class FavoriteSideItemListDTO
    {
        public string UserId { get; set; }
        public int SideItemId { get; set; }
        public string SideItemName { get; set; } // SideItem'in adını listelemek için
        public DateTime AddedDate { get; set; }
        public string Notes { get; set; }
    }
}
