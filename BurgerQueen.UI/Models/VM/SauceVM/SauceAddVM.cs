using BurgerQueen.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.SauceVM
{
    public class SauceAddVM
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

        public SauceAddDTO ToSauceAddDTO()
        {
            return new SauceAddDTO
            {
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                ImagePath = this.ImagePath
            };
        }
    }
}