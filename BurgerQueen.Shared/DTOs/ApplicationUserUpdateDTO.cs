using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Shared.DTOs
{
    public class ApplicationUserUpdateDTO
    {
        public string Id { get; set; } // Güncelleme için Id gereklidir
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? DateOfBirth { get; set; } // Nullable yaparak güncelleme esnekliği sağlanır
        public bool IsSubscribedToNewsletter { get; set; }
        public string PreferredLanguage { get; set; }
        // Password'u güncelleme için burada tutmayacağız, ayrı bir endpoint olabilir
    }
}
