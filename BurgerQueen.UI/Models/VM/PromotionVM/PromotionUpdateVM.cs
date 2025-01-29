using BurgerQueen.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.PromotionVM
{
    public class PromotionUpdateVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal DiscountPercentage { get; set; }

        public bool IsActive { get; set; }

        public PromotionUpdateDTO ToPromotionUpdateDTO()
        {
            return new PromotionUpdateDTO
            {
                Id = this.Id,
                Name = this.Name,
                Description = this.Description,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                DiscountPercentage = this.DiscountPercentage,
                IsActive = this.IsActive
            };
        }
    }
}