using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class PasswordUpdateDTO
    {
        public string UserId { get; set; } // Kullanıcının ID'si
        public string CurrentPassword { get; set; } // Mevcut şifre
        public string NewPassword { get; set; } // Yeni şifre
    }
}
