using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerQueen.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DrinkController : Controller
    {
        private readonly IDrinkService _drinkService;

        public DrinkController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }

        public async Task<IActionResult> Index()
        {
            var drinks = await _drinkService.GetDrinksAll();
            return View(drinks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var drink = await _drinkService.GetDrinkById(id);
            if (drink == null)
            {
                return NotFound();
            }
            return View(drink);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DrinkAddVM model)
        {
            if (ModelState.IsValid)
            {
                await _drinkService.AddDrinkDTO(model.ToDrinkAddDTO()); // ViewModel'den DTO'ya dönüşüm
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var drink = await _drinkService.GetDrinkById(id);
            if (drink == null)
            {
                return NotFound();
            }
            return View(drink.ToDrinkUpdateVM()); // DTO'dan ViewModel'e dönüşüm
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DrinkUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _drinkService.UpdateDrinkDTO(model.ToDrinkUpdateDTO()); // ViewModel'den DTO'ya dönüşüm
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "İçecek güncellenirken bir hata oluştu.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var drink = await _drinkService.GetDrinkById(id);
            if (drink == null)
            {
                return NotFound();
            }
            return View(drink);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _drinkService.DeleteDrinkDTO(id);
            return RedirectToAction(nameof(Index));
        }
    }
}