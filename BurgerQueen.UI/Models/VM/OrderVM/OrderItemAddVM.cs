using BurgerQueen.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.OrderVM
{
    public class OrderItemAddVM
    {
        public int? MenuId { get; set; }
        public int? BurgerId { get; set; }
        public int? DrinkId { get; set; }
        public int? FriesId { get; set; }
        public int? SideItemId { get; set; }
        public int? SauceId { get; set; }
        public List<int> ExtraIngredientIds { get; set; }

        [StringLength(50)]
        public string BurgerSize { get; set; }

        [StringLength(50)]
        public string DrinkSize { get; set; }

        [StringLength(50)]
        public string FriesSize { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(1000)]
        public string Customizations { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public OrderItemAddDTO ToOrderItemAddDTO()
        {
            return new OrderItemAddDTO
            {
                MenuId = this.MenuId,
                BurgerId = this.BurgerId,
                DrinkId = this.DrinkId,
                FriesId = this.FriesId,
                SideItemId = this.SideItemId,
                SauceId = this.SauceId,
                ExtraIngredientIds = this.ExtraIngredientIds,
                BurgerSize = this.BurgerSize,
                DrinkSize = this.DrinkSize,
                FriesSize = this.FriesSize,
                Quantity = this.Quantity,
                Customizations = this.Customizations,
                Price = this.Price
            };
        }
    }
}