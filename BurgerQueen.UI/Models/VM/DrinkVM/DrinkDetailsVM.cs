using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.DrinkVM
{
    public class DrinkDetailsVM
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

        public bool IsSugary { get; set; }
        public bool IsAlcoholic { get; set; }
        public int? Calories { get; set; }
        public string Sizes { get; set; }
        public string Ingredients { get; set; }
    }
}