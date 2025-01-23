using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class FavoriteSauceAddDTO
    {
        public string UserId { get; set; }
        public int SauceId { get; set; }
    }
}
