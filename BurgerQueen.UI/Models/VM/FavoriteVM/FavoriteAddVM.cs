namespace BurgerQueen.UI.Models.VM.FavoriteVM
{
    public class FavoriteAddVM
    {
        public FavoriteType FavoriteType { get; set; }
        public int ItemId { get; set; }
    }

    public enum FavoriteType
    {
        Burger,
        Drink,
        Fry,
        Menu,
        SideItem,
        Sauce
    }
}