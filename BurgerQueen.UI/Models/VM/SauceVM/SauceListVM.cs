using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.SauceVM
{
    public class SauceListVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}