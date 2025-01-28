using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace BurgerQueen.UI.Models.VM.ApplicationUserVM
{
    public class Disable2faVM
    {
        [Display(Name = "İki Faktörlü Doğrulamayı Devre Dışı Bırak")]
        public bool ConfirmDisable { get; set; }
        public string TwoFactorCode { get; set; }

    }
}
