using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerQueen.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BurgerController : Controller
    {
        private readonly IBurgerService _burgerService;

        public BurgerController(IBurgerService burgerService)
        {
            _burgerService = burgerService;
        }

        public async Task<IActionResult> Index()
        {
            var burgers = await _burgerService.GetBurgersAll();
            return View(burgers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var burger = await _burgerService.GetBurgerById(id);
            if (burger == null)
            {
                return NotFound();
            }
            return View(burger);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BurgerAddDTO model)
        {
            if (ModelState.IsValid)
            {
                await _burgerService.AddBurgerDTO(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var burger = await _burgerService.GetBurgerById(id);
            if (burger == null)
            {
                return NotFound();
            }
            return View(burger);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BurgerUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _burgerService.UpdateBurgerDTO(model);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Burger güncellenirken bir hata oluştu.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var burger = await _burgerService.GetBurgerById(id);
            if (burger == null)
            {
                return NotFound();
            }
            return View(burger);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _burgerService.DeleteBurgerDTO(id);
            return RedirectToAction(nameof(Index));
        }
    }
}