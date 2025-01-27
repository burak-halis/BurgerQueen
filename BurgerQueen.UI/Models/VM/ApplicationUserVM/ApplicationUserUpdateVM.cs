using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.ApplicationUserVM
{
    public class ApplicationUserUpdateVM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olabilir.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur.")]
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olabilir.")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "Adres en fazla 100 karakter olabilir.")]
        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public string ProfilePictureUrl { get; set; }

        // Eğer dosya yükleme özelliği eklemek isterseniz:
        [Display(Name = "Profil Resmi")]
        public IFormFile ProfilePicture { get; set; }
    }
}
