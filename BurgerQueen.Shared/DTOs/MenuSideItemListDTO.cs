using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class MenuSideItemListDTO
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int SideItemId { get; set; }
    }
}
