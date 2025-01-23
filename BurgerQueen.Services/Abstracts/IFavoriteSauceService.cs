using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IFavoriteSauceService : IBaseService<FavoriteSauce>
    {
        Task AddFavoriteSauceAsync(FavoriteSauceAddDTO dto);
        Task UpdateFavoriteSauceAsync(FavoriteSauceUpdateDTO dto);
        Task DeleteFavoriteSauceAsync(int sauceId, string userId);
        Task<FavoriteSauceUpdateDTO> GetFavoriteSauceByIdAsync(int sauceId, string userId);
        Task<List<FavoriteSauceListDTO>> GetAllFavoriteSaucesAsync(string userId);
    }
}
