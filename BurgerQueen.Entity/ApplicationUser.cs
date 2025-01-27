using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Entity
{
    public class ApplicationUser : IdentityUser
    {
        // Kullanıcı profili bilgileri
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; } // IdentityUser'da zaten var ama burada tekrar belirtiyoruz

        // Kullanıcının tercihleri veya ek bilgiler
        public string? ProfilePictureUrl { get; set; }
        public bool? IsSubscribedToNewsletter { get; set; }
        public string? PreferredLanguage { get; set; } // "en", "tr" gibi

        // Kullanıcının siparişleri
        public virtual ICollection<Order> Orders { get; set; }

        // Kullanıcının favori ürünleri
        public virtual ICollection<FavoriteBurger> FavoriteBurgers { get; set; }
        public virtual ICollection<FavoriteDrink> FavoriteDrinks { get; set; }
        public virtual ICollection<FavoriteFry> FavoriteFries { get; set; }
        public virtual ICollection<FavoriteSideItem> FavoriteSideItems { get; set; }
        public virtual ICollection<FavoriteSauce> FavoriteSauces { get; set; }
        public virtual ICollection<FavoriteMenu> FavoriteMenus { get; set; }

        // Diğer olası ilişkiler veya ek alanlar
        public DateTime? LastLoginDate { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
        public bool? IsActive { get; set; } = true;
    }

}
