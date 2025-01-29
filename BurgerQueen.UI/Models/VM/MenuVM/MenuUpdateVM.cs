using BurgerQueen.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.MenuVM
{
    public class MenuUpdateVM
    {
        [Key]
        public int Id { get; set; }

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

        public MenuUpdateDTO ToMenuUpdateDTO()
        {
            return new MenuUpdateDTO
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                TotalPrice = this.TotalPrice,
                ImagePath = this.ImagePath,
                MenuType = this.MenuType
            };
        }
    }
}