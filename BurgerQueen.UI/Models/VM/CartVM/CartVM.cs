namespace BurgerQueen.UI.Models.VM.CartVM
{
    public class CartVM
    {
        public List<CartItemVM> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string SpecialInstructions { get; set; }
    }
}