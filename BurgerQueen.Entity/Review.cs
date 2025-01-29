using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class Review : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public int ProductId { get; set; }

        // Tüm olası ürün türlerine ilişkilendirme yapabiliriz, ancak bu her zaman kullanışlı olmayabilir
        public int? BurgerId { get; set; }
        [ForeignKey("BurgerId")]
        public Burger Burger { get; set; }

        public int? DrinkId { get; set; }
        [ForeignKey("DrinkId")]
        public Drink Drink { get; set; }

        public int? FryId { get; set; }
        [ForeignKey("FryId")]
        public Fry Fry { get; set; }

        public int? SideItemId { get; set; }
        [ForeignKey("SideItemId")]
        public SideItem SideItem { get; set; }

        public int? SauceId { get; set; }
        [ForeignKey("SauceId")]
        public Sauce Sauce { get; set; }

        public int? MenuId { get; set; }
        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
    }
}
