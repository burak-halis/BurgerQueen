using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class CartDTO
    {
        public List<CartItemDTO> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
