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
    public class FryController : Controller
    {
        private readonly IFryService _fryService;

        public FryController(IFryService fryService)
        {
            _fryService = fryService;
        }

        public async Task<IActionResult> Index()
        {
            var fries = await _fryService.GetFriesAll();
            return View(fries);
        }

        public async Task<IActionResult> Details(int id)
        {
            var fry = await _fryService.GetFryById(id);
            if (fry == null)
            {
                return NotFound();
            }
            return View(fry);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FryAddVM model)
        {
            if (ModelState.IsValid)
            {
                await _fryService.AddFryDTO(model.ToFryAddDTO()); // ViewModel'den DTO'ya dönüşüm
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var fry = await _fryService.GetFryById(id);
            if (fry == null)
            {
                return NotFound();
            }
            return View(fry.ToFryUpdateVM()); // DTO'dan ViewModel'e dönüşüm
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FryUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _fryService.UpdateFryDTO(model.ToFryUpdateDTO()); // ViewModel'den DTO'ya dönüşüm
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Patates kızartması güncellenirken bir hata oluştu.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var fry = await _fryService.GetFryById(id);
            if (fry == null)
            {
                return NotFound();
            }
            return View(fry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _fryService.DeleteFryDTO(id);
            return RedirectToAction(nameof(Index));
        }
    }
}