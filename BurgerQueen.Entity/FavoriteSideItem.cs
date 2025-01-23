﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class FavoriteSideItem : BaseEntity
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        public int SideItemId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("SideItemId")]
        public virtual SideItem SideItem { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; }
    }
}
