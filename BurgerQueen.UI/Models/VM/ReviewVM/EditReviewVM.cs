using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.ReviewVM
{
    public class EditReviewVM
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

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