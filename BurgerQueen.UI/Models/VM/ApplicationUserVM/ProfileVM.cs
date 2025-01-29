using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.ApplicationUserVM
{
    using BurgerQueen.Shared.Enums;
    using System.ComponentModel.DataAnnotations;

    public class ProfileVM
    {
        public string Id { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [EmailAddress]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "Adres en fazla 100 karakter olabilir.")]
        [Display(Name = "Adres")]
        public string? Address { get; set; }

        public Gender? Gender { get; set; }

        [Phone]
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Profil Resmi URL'si")]
        public string ProfilePictureUrl { get; set; }
    }
}