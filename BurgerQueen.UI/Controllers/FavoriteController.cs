using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.UI.Models.VM.FavoriteVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BurgerQueen.UI.Controllers
{
    [Authorize(Roles = "User")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteBurgerService _favoriteBurgerService;
        private readonly IFavoriteDrinkService _favoriteDrinkService;
        private readonly IFavoriteFryService _favoriteFryService;
        private readonly IFavoriteMenuService _favoriteMenuService;
        private readonly IFavoriteSideItemService _favoriteSideItemService;
        private readonly IFavoriteSauceService _favoriteSauceService;

        public FavoriteController(
            IFavoriteBurgerService favoriteBurgerService,
            IFavoriteDrinkService favoriteDrinkService,
            IFavoriteFryService favoriteFryService,
            IFavoriteMenuService favoriteMenuService,
            IFavoriteSideItemService favoriteSideItemService,
            IFavoriteSauceService favoriteSauceService)
        {
            _favoriteBurgerService = favoriteBurgerService;
            _favoriteDrinkService = favoriteDrinkService;
            _favoriteFryService = favoriteFryService;
            _favoriteMenuService = favoriteMenuService;
            _favoriteSideItemService = favoriteSideItemService;
            _favoriteSauceService = favoriteSauceService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favoritesVM = new FavoritesVM
            {
                Burgers = await _favoriteBurgerService.GetAllFavoriteBurgersAsync(userId),
                Drinks = await _favoriteDrinkService.GetAllFavoriteDrinksAsync(userId),
                Fries = await _favoriteFryService.GetAllFavoriteFriesAsync(userId),
                Menus = await _favoriteMenuService.GetAllFavoriteMenusAsync(userId),
                SideItems = await _favoriteSideItemService.GetAllFavoriteSideItemsAsync(userId),
                Sauces = await _favoriteSauceService.GetAllFavoriteSaucesAsync(userId)
            };
            return View(favoritesVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(FavoriteAddVM model)
        {
            if (ModelState.IsValid)
            {
                switch (model.FavoriteType)
                {
                    case FavoriteType.Burger:
                        await _favoriteBurgerService.AddFavoriteBurgerAsync(new FavoriteBurgerAddDTO { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), BurgerId = model.ItemId });
                        break;
                    case FavoriteType.Drink:
                        await _favoriteDrinkService.AddFavoriteDrinkAsync(new FavoriteDrinkAddDTO { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), DrinkId = model.ItemId });
                        break;
                    case FavoriteType.Fry:
                        await _favoriteFryService.AddFavoriteFryAsync(new FavoriteFryAddDTO { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), FriesId = model.ItemId });
                        break;
                    case FavoriteType.Menu:
                        await _favoriteMenuService.AddFavoriteMenuAsync(new FavoriteMenuAddDTO { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), MenuId = model.ItemId });
                        break;
                    case FavoriteType.SideItem:
                        await _favoriteSideItemService.AddFavoriteSideItemAsync(new FavoriteSideItemAddDTO { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), SideItemId = model.ItemId });
                        break;
                    case FavoriteType.Sauce:
                        await _favoriteSauceService.AddFavoriteSauceAsync(new FavoriteSauceAddDTO { UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), SauceId = model.ItemId });
                        break;
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(FavoriteRemoveVM model)
        {
            if (ModelState.IsValid)
            {
                switch (model.FavoriteType)
                {
                    case FavoriteType.Burger:
                        await _favoriteBurgerService.DeleteFavoriteBurgerAsync(model.ItemId, User.FindFirstValue(ClaimTypes.NameIdentifier));
                        break;
                    case FavoriteType.Drink:
                        await _favoriteDrinkService.DeleteFavoriteDrinkAsync(model.ItemId, User.FindFirstValue(ClaimTypes.NameIdentifier));
                        break;
                    case FavoriteType.Fry:
                        await _favoriteFryService.DeleteFavoriteFryAsync(model.ItemId, User.FindFirstValue(ClaimTypes.NameIdentifier));
                        break;
                    case FavoriteType.Menu:
                        await _favoriteMenuService.DeleteFavoriteMenuAsync(model.ItemId, User.FindFirstValue(ClaimTypes.NameIdentifier));
                        break;
                    case FavoriteType.SideItem:
                        await _favoriteSideItemService.DeleteFavoriteSideItemAsync(model.ItemId, User.FindFirstValue(ClaimTypes.NameIdentifier));
                        break;
                    case FavoriteType.Sauce:
                        await _favoriteSauceService.DeleteFavoriteSauceAsync(model.ItemId, User.FindFirstValue(ClaimTypes.NameIdentifier));
                        break;
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}