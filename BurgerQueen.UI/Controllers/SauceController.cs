using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.UI.Models.VM.SauceVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerQueen.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SauceController : Controller
    {
        private readonly ISauceService _sauceService;

        public SauceController(ISauceService sauceService)
        {
            _sauceService = sauceService;
        }

        public async Task<IActionResult> Index()
        {
            var sauces = await _sauceService.GetSaucesAll();
            return View(sauces);
        }

        public async Task<IActionResult> Details(int id)
        {
            var sauce = await _sauceService.GetSauceById(id);
            if (sauce == null)
            {
                return NotFound();
            }
            return View(sauce);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SauceAddVM model)
        {
            if (ModelState.IsValid)
            {
                await _sauceService.AddSauceDTO(model.ToSauceAddDTO()); // ViewModel'den DTO'ya dönüşüm
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sauce = await _sauceService.GetSauceById(id);
            if (sauce == null)
            {
                return NotFound();
            }
            return View(sauce.ToSauceUpdateVM()); // DTO'dan ViewModel'e dönüşüm
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SauceUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sauceService.UpdateSauceDTO(model.ToSauceUpdateDTO()); // ViewModel'den DTO'ya dönüşüm
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Sos güncellenirken bir hata oluştu.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sauce = await _sauceService.GetSauceById(id);
            if (sauce == null)
            {
                return NotFound();
            }
            return View(sauce);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sauceService.DeleteSauceDTO(id);
            return RedirectToAction(nameof(Index));
        }
    }
}