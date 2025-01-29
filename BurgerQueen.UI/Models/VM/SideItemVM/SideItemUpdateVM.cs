using BurgerQueen.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.SideItemVM
{
    public class SideItemUpdateVM
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

        public bool IsVegetarian { get; set; }
        public bool IsGlutenFree { get; set; }
        public int? Calories { get; set; }

        public SideItemUpdateDTO ToSideItemUpdateDTO()
        {
            return new SideItemUpdateDTO
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                Price = this.Price,
                ImagePath = this.ImagePath,
                IsVegetarian = this.IsVegetarian,
                IsGlutenFree = this.IsGlutenFree,
                Calories = this.Calories
            };
        }
    }
}