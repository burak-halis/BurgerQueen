﻿using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.SideItemVM
{
    public class SideItemDetailsVM
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

        public bool IsVegetarian { get; set; }
        public bool IsGlutenFree { get; set; }
        public int? Calories { get; set; }
    }
}