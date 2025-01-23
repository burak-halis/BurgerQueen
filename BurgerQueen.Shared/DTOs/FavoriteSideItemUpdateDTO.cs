using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class FavoriteSideItemUpdateDTO : FavoriteSideItemAddDTO
    {
        public string Notes { get; set; }
        // Diğer güncellenebilir alanlar buraya eklenebilir
    }
}
