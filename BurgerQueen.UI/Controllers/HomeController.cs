using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.UI.Models;
using BurgerQueen.UI.Models.VM;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BurgerQueen.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerQueen.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBurgerService _burgerService;
        private readonly IMenuService _menuService;
        private readonly IDrinkService _drinkService;
        private readonly IFryService _fryService;
        private readonly ISideItemService _sideItemService;
        private readonly ISauceService _sauceService;

        public HomeController(
            IBurgerService burgerService,
            IMenuService menuService,
            IDrinkService drinkService,
            IFryService fryService,
            ISideItemService sideItemService,
            ISauceService sauceService)
        {
            _burgerService = burgerService;
            _menuService = menuService;
            _drinkService = drinkService;
            _fryService = fryService;
            _sideItemService = sideItemService;
            _sauceService = sauceService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                FeaturedBurgers = await GetFeaturedBurgers(),
                FeaturedMenus = await GetFeaturedMenus(),
                FeaturedDrinks = await GetFeaturedDrinks(),
                FeaturedFries = await GetFeaturedFries(),
                FeaturedSideItems = await GetFeaturedSideItems(),
                FeaturedSauces = await GetFeaturedSauces(),
                WeeklySpecial = await GetWeeklySpecial()
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<List<BurgerListDTO>> GetFeaturedBurgers()
        {
            var burgers = await _burgerService.GetBurgersAll();
            return burgers.Where(b => b.Popularity > 4).Select(b => new BurgerListDTO
            {
                Id = b.Id,
                Name = b.Name,
                Price = b.Price,
                ImagePath = b.ImagePath,
                Popularity = b.Popularity // Entity'den DTO'ya bu özelliði ekledik
            }).Take(4).ToList();
        }

        private async Task<List<MenuListDTO>> GetFeaturedMenus()
        {
            var menus = await _menuService.GetMenusAll();
            return menus.Where(m => m.Discount.HasValue).Select(m => new MenuListDTO
            {
                Id = m.Id,
                Name = m.Name,
                TotalPrice = m.TotalPrice,
                ImagePath = m.ImagePath,
                Discount = m.Discount // Entity'den DTO'ya bu özelliði ekledik
            }).Take(3).ToList();
        }

        private async Task<List<DrinkListDTO>> GetFeaturedDrinks()
        {
            var drinks = await _drinkService.GetDrinksAll();
            return drinks.Where(d => d.IsSugary == false).Select(d => new DrinkListDTO
            {
                Id = d.Id,
                Name = d.Name,
                Price = d.Price,
                ImagePath = d.ImagePath,
                IsSugary = d.IsSugary // Entity'den DTO'ya bu özelliði ekledik
            }).Take(3).ToList();
        }

        private async Task<List<FryListDTO>> GetFeaturedFries()
        {
            var fries = await _fryService.GetFriesAll();
            return fries.Where(f => f.IsSpicy == true).Select(f => new FryListDTO
            {
                Id = f.Id,
                Name = f.Name,
                Price = f.Price,
                ImagePath = f.ImagePath,
                IsSpicy = f.IsSpicy // Entity'den DTO'ya bu özelliði ekledik
            }).Take(2).ToList();
        }

        private async Task<List<SideItemListDTO>> GetFeaturedSideItems()
        {
            var sideItems = await _sideItemService.GetSideItemsAll();
            return sideItems.Where(s => s.IsVegetarian == true).Select(s => new SideItemListDTO
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                ImagePath = s.ImagePath,
                IsVegetarian = s.IsVegetarian // Entity'den DTO'ya bu özelliði ekledik
            }).Take(2).ToList();
        }

        private async Task<List<SauceListDTO>> GetFeaturedSauces()
        {
            var sauces = await _sauceService.GetSaucesAll();
            return sauces.Select(s => new SauceListDTO
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                ImagePath = s.ImagePath
            }).Take(2).ToList();
        }

        private async Task<MenuListDTO?> GetWeeklySpecial()
        {
            var menus = await _menuService.GetMenusAll();
            return menus.Where(m => m.Discount.HasValue)
                        .Select(m => new MenuListDTO
                        {
                            Id = m.Id,
                            Name = m.Name,
                            TotalPrice = m.TotalPrice,
                            ImagePath = m.ImagePath,
                            Discount = m.Discount
                        })
                        .FirstOrDefault();
        }
    }

    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
