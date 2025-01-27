using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.ApplicationUserVM
{
    public class PasswordUpdateVM
    {
        [Required(ErrorMessage = "E-posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi giriniz.")]
        [Display(Name = "E-posta Adresi")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mevcut şifre zorunludur.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mevcut Şifre")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Yeni şifre zorunludur.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Yeni şifre en az 8 karakter olmalıdır.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Yeni şifre en az bir büyük harf, bir küçük harf, bir sayı ve bir özel karakter içermelidir.")]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Şifre onayı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Yeni şifreler eşleşmiyor.")]
        [Display(Name = "Şifre Onayı")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Token gereklidir.")]
        [HiddenInput]
        public string Token { get; set; }
    }
}
