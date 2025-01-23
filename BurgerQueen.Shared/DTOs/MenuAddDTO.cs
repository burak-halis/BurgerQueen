using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class MenuAddDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? TotalPrice { get; set; }
        public string ImagePath { get; set; }
        public string MenuType { get; set; }
    }
}
