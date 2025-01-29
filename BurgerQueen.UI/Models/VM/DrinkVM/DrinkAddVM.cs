using BurgerQueen.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.DrinkVM
{
    public class DrinkAddVM
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public bool IsSugary { get; set; }
        public bool IsAlcoholic { get; set; }
        public int? Calories { get; set; }
        public string Sizes { get; set; }
        public string Ingredients { get; set; }

        public DrinkAddDTO ToDrinkAddDTO()
        {
            return new DrinkAddDTO
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                ImagePath = this.ImagePath,
                IsSugary = this.IsSugary,
                IsAlcoholic = this.IsAlcoholic,
                Calories = this.Calories,
                Sizes = this.Sizes,
                Ingredients = this.Ingredients
            };
        }
    }
}