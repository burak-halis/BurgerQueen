using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class FavoriteBurgerAddDTO
    {
        public string UserId { get; set; }
        public int BurgerId { get; set; }
    }
}
