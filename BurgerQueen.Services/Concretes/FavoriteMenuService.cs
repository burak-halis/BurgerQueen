using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Concretes
{
    public class FavoriteMenuService : BaseService<FavoriteMenu>, IFavoriteMenuService
    {
        public FavoriteMenuService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddFavoriteMenuAsync(FavoriteMenuAddDTO dto)
        {
            var favoriteMenu = new FavoriteMenu
            {
                UserId = dto.UserId,
                MenuId = dto.MenuId,
                AddedDate = DateTime.UtcNow
            };
            await base.AddDTOAsync(favoriteMenu);
        }

        public async Task UpdateFavoriteMenuAsync(FavoriteMenuUpdateDTO dto)
        {
            var favoriteMenu = await base.GetByIdDTO(fm => fm.UserId == dto.UserId && fm.MenuId == dto.MenuId);
            if (favoriteMenu != null)
            {
                favoriteMenu.Notes = dto.Notes;
                await base.UpdateDTOAsync(favoriteMenu);
            }
            else
            {
                throw new Exception("Favori menü bulunamadı.");
            }
        }

        public async Task DeleteFavoriteMenuAsync(int menuId, string userId)
        {
            var favoriteMenu = await base.GetByIdDTO(fm => fm.UserId == userId && fm.MenuId == menuId);
            if (favoriteMenu != null)
            {
                await base.DeleteDTOAsync(favoriteMenu.Id);
            }
            else
            {
                throw new Exception("Favori menü bulunamadı.");
            }
        }

        public async Task<FavoriteMenuUpdateDTO> GetFavoriteMenuByIdAsync(int menuId, string userId)
        {
            var favoriteMenu = await base.GetByIdDTO(fm => fm.UserId == userId && fm.MenuId == menuId);
            if (favoriteMenu != null)
            {
                return new FavoriteMenuUpdateDTO
                {
                    UserId = favoriteMenu.UserId,
                    MenuId = favoriteMenu.MenuId,
                    Notes = favoriteMenu.Notes
                };
            }
            return null;
        }

        public async Task<List<FavoriteMenuListDTO>> GetAllFavoriteMenusAsync(string userId)
        {
            var favoriteMenus = await base.GetByDTO(fm => fm.UserId == userId);
            return favoriteMenus.Select(fm => new FavoriteMenuListDTO
            {
                UserId = fm.UserId,
                MenuId = fm.MenuId,
                MenuName = fm.Menu.Name, // Navigasyon özelliği varsa, burada kullanılır
                AddedDate = fm.AddedDate,
                Notes = fm.Notes
            }).ToList();
        }
    }
}
