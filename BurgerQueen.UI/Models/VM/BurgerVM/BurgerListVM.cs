using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.BurgerVM
{
    public class BurgerListVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public int? Popularity { get; set; } // Popülerlik seviyesi
    }
}