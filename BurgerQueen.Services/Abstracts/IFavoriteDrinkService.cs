using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IFavoriteDrinkService : IBaseService<FavoriteDrink>
    {
        Task AddFavoriteDrinkAsync(FavoriteDrinkAddDTO dto);
        Task UpdateFavoriteDrinkAsync(FavoriteDrinkUpdateDTO dto);
        Task DeleteFavoriteDrinkAsync(int drinkId, string userId);
        Task<FavoriteDrinkUpdateDTO> GetFavoriteDrinkByIdAsync(int drinkId, string userId);
        Task<List<FavoriteDrinkListDTO>> GetAllFavoriteDrinksAsync(string userId);
    }
}
