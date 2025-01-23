using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class FavoriteMenuListDTO
    {
        public string UserId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; } // Menu'nun adını listelemek için
        public DateTime AddedDate { get; set; }
        public string Notes { get; set; }
    }
}
