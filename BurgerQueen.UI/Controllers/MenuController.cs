using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.UI.Models.VM.MenuVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerQueen.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IMenuBurgerService _menuBurgerService;
        private readonly IMenuDrinkService _menuDrinkService;
        private readonly IMenuFryService _menuFryService;
        private readonly IMenuSideItemService _menuSideItemService;

        public MenuController(
            IMenuService menuService,
            IMenuBurgerService menuBurgerService,
            IMenuDrinkService menuDrinkService,
            IMenuFryService menuFryService,
            IMenuSideItemService menuSideItemService)
        {
            _menuService = menuService;
            _menuBurgerService = menuBurgerService;
            _menuDrinkService = menuDrinkService;
            _menuFryService = menuFryService;
            _menuSideItemService = menuSideItemService;
        }

        public async Task<IActionResult> Index()
        {
            var menus = await _menuService.GetMenusAll();
            return View(menus);
        }

        public async Task<IActionResult> Details(int id)
        {
            var menu = await _menuService.GetMenuById(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuAddVM model)
        {
            if (ModelState.IsValid)
            {
                await _menuService.AddMenuDTO(model.ToMenuAddDTO()); // ViewModel'den DTO'ya dönüşüm
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var menu = await _menuService.GetMenuById(id);
            if (menu == null)
            {
                return NotFound();
            }

            // DTO'dan ViewModel'e dönüşüm burada yapılır
            var menuVM = new MenuUpdateVM
            {
                Id = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                TotalPrice = menu.TotalPrice,
                ImagePath = menu.ImagePath,
                MenuType = menu.MenuType
            };

            return View(menuVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MenuUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _menuService.UpdateMenuDTO(model.ToMenuUpdateDTO()); // ViewModel'den DTO'ya dönüşüm
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Menü güncellenirken bir hata oluştu.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var menu = await _menuService.GetMenuById(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _menuService.DeleteMenuDTO(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetMenuItems(int menuId)
        {
            var items = new MenuItemsVM
            {
                Burgers = await _menuBurgerService.GetBurgersForMenuAsync(menuId),
                Drinks = await _menuDrinkService.GetDrinksForMenuAsync(menuId),
                Fries = await _menuFryService.GetFriesForMenuAsync(menuId),
                SideItems = await _menuSideItemService.GetSideItemsForMenuAsync(menuId)
            };
            return View(items);
        }
    }
}