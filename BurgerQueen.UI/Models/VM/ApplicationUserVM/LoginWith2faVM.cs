using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.ApplicationUserVM
{
    public class LoginWith2faVM
    {
        [Required(ErrorMessage = "Doğrulama kodu zorunludur.")]
        [StringLength(7, ErrorMessage = "Doğrulama kodu 6 karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [Display(Name = "Doğrulama Kodu")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Bu cihazı hatırla")]
        public bool RememberMachine { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool RememberMe { get; set; }

        [Display(Name = "Geri Dönüş URL'si")]
        public string ReturnUrl { get; set; }
    }
}
