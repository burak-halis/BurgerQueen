using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.MenuVM
{
    public class MenuListVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal? TotalPrice { get; set; }

        [Required]
        public string ImagePath { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Discount { get; set; }
    }
}