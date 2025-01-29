using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.BurgerVM
{
    public class BurgerDetailsVM
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int? Calories { get; set; }

        public bool IsVegetarian { get; set; }

        public bool IsGlutenFree { get; set; }

        [StringLength(1000)]
        public string Ingredients { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public int? Popularity { get; set; } // Popülerlik seviyesi
    }
}