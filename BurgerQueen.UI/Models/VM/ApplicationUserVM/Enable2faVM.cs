using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.ApplicationUserVM
{
    public class Enable2faVM
    {
        [Required]
        [StringLength(7, ErrorMessage = "Kod 6 karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [Display(Name = "Doğrulama Kodu")]
        public string Code { get; set; }

        public string SharedKey { get; set; }

        public string AuthenticatorUri { get; set; }
    }
}
