using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class DrinkUpdateDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Required]
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
