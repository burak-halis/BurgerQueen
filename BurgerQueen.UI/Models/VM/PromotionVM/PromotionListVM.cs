namespace BurgerQueen.UI.Models.VM.PromotionVM
{
    public class PromotionListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal DiscountPercentage { get; set; }
        public bool IsActive { get; set; }
    }
}