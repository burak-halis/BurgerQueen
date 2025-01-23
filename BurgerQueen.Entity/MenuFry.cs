using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class MenuFry : BaseEntity
    {
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int FryId { get; set; }
        public Fry Fry { get; set; }
    }
}
