using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.SauceVM
{
    public class SauceDetailsVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string ImagePath { get; set; }
    }
}