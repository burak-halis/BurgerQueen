using BurgerQueen.Shared.DTOs;

namespace BurgerQueen.UI.Models.VM.FavoriteVM
{
    public class FavoritesVM
    {
        public List<FavoriteBurgerListDTO> Burgers { get; set; }
        public List<FavoriteDrinkListDTO> Drinks { get; set; }
        public List<FavoriteFryListDTO> Fries { get; set; }
        public List<FavoriteMenuListDTO> Menus { get; set; }
        public List<FavoriteSideItemListDTO> SideItems { get; set; }
        public List<FavoriteSauceListDTO> Sauces { get; set; }
    }
}