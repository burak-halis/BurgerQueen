using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.FryVM
{
    public class FryListVM
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

        public bool IsSpicy { get; set; }
    }
}