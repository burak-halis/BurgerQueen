using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.ApplicationUserVM
{
    public class ProfileVM
    {
        [Display(Name = "Kullanıcı ID")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Adınız 50 karakterden uzun olamaz.")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Soyadınız 50 karakterden uzun olamaz.")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }
    }
}