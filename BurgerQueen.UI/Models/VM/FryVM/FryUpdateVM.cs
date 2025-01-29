using BurgerQueen.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.FryVM
{
    public class FryUpdateVM
    {
        [Key]
        public int Id { get; set; }

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

        public bool IsSpicy { get; set; }
        public int? Calories { get; set; }
        public string Sizes { get; set; }
        public string Type { get; set; }

        public FryUpdateDTO ToFryUpdateDTO()
        {
            return new FryUpdateDTO
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                ImagePath = this.ImagePath,
                IsSpicy = this.IsSpicy,
                Calories = this.Calories,
                Sizes = this.Sizes,
                Type = this.Type
            };
        }
    }
}