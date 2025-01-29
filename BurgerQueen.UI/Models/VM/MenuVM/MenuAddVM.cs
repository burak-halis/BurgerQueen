using BurgerQueen.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.MenuVM
{
    public class MenuAddVM
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal? TotalPrice { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [Required]
        [StringLength(50)]
        public string MenuType { get; set; }

        public MenuAddDTO ToMenuAddDTO()
        {
            return new MenuAddDTO
            {
                Name = this.Name,
                Description = this.Description,
                TotalPrice = this.TotalPrice,
                ImagePath = this.ImagePath,
                MenuType = this.MenuType
            };
        }
    }
}