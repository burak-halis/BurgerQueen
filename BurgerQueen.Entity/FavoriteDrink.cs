using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class FavoriteDrink : BaseEntity
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int DrinkId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("DrinkId")]
        public virtual Drink Drink { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; }
    }
}
