using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.ApplicationUserVM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "E-posta veya kullanıcı adı zorunludur.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool RememberMe { get; set; }
    }
}
