using BurgerQueen.Shared.DTOs;

namespace BurgerQueen.UI.Models.VM.MenuVM
{
    public class MenuItemsVM
    {
        public IEnumerable<MenuBurgerListDTO> Burgers { get; set; }
        public IEnumerable<MenuDrinkListDTO> Drinks { get; set; }
        public IEnumerable<MenuFryListDTO> Fries { get; set; }
        public IEnumerable<MenuSideItemListDTO> SideItems { get; set; }
    }
}