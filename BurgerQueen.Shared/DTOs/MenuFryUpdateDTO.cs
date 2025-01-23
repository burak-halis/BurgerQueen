using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class MenuFryUpdateDTO
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int FryId { get; set; }
    }
}
