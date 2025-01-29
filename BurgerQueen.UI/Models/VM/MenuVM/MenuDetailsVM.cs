using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.MenuVM
{
    public class MenuDetailsVM
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

        [StringLength(50)]
        public string MenuType { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Discount { get; set; }
    }
}