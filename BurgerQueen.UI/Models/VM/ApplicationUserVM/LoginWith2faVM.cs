using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.ApplicationUserVM
{
    public class LoginWith2faVM
    {
        [Required]
        [Display(Name = "Doğrulama Kodu")]
        public string TwoFactorCode { get; set; }

        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
