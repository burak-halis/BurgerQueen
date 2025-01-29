using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.ReviewVM
{
    public class AddReviewVM
    {
        [Required]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}