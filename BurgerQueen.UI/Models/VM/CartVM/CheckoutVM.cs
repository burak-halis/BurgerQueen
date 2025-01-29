using BurgerQueen.Shared.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BurgerQueen.UI.Models.VM.CartVM
{
    public class CheckoutVM
    {
        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public string SpecialInstructions { get; set; }

        public CheckoutDTO ToCheckoutDTO()
        {
            return new CheckoutDTO
            {
                ShippingAddress = this.ShippingAddress,
                PaymentMethod = this.PaymentMethod,
                SpecialInstructions = this.SpecialInstructions
            };
        }
    }
}