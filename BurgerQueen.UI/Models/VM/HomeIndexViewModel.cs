using BurgerQueen.Shared.DTOs;

namespace BurgerQueen.UI.Models.VM
{
    public class HomeIndexViewModel
    {
        public List<BurgerListDTO> FeaturedBurgers { get; set; }
        public List<MenuListDTO> FeaturedMenus { get; set; }
        public List<DrinkListDTO> FeaturedDrinks { get; set; }
        public List<FryListDTO> FeaturedFries { get; set; }
        public List<SideItemListDTO> FeaturedSideItems { get; set; }
        public List<SauceListDTO> FeaturedSauces { get; set; }
        public MenuListDTO WeeklySpecial { get; set; }
    }
}
