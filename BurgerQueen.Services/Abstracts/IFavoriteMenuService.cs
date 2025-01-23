using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IFavoriteMenuService : IBaseService<FavoriteMenu>
    {
        Task AddFavoriteMenuAsync(FavoriteMenuAddDTO dto);
        Task UpdateFavoriteMenuAsync(FavoriteMenuUpdateDTO dto);
        Task DeleteFavoriteMenuAsync(int menuId, string userId);
        Task<FavoriteMenuUpdateDTO> GetFavoriteMenuByIdAsync(int menuId, string userId);
        Task<List<FavoriteMenuListDTO>> GetAllFavoriteMenusAsync(string userId);
    }
}
