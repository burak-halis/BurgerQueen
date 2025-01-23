using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class OrderItemUpdateDTO
    {
        public int Id { get; set; }
        public int? MenuId { get; set; }
        public int? BurgerId { get; set; }
        public int? DrinkId { get; set; }
        public int? FriesId { get; set; }
        public int? SideItemId { get; set; }
        public int? SauceId { get; set; }
        public List<int> ExtraIngredientIds { get; set; }
        public string BurgerSize { get; set; }
        public string DrinkSize { get; set; }
        public string FriesSize { get; set; }
        public int Quantity { get; set; }
        public string Customizations { get; set; }
        public decimal Price { get; set; }
    }
}
