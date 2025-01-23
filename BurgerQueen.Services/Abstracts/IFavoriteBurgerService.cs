using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IFavoriteBurgerService : IBaseService<FavoriteBurger>
    {
        Task AddFavoriteBurgerAsync(FavoriteBurgerAddDTO dto);
        Task UpdateFavoriteBurgerAsync(FavoriteBurgerUpdateDTO dto);
        Task DeleteFavoriteBurgerAsync(int burgerId, string userId);
        Task<FavoriteBurgerUpdateDTO> GetFavoriteBurgerByIdAsync(int burgerId, string userId);
        Task<List<FavoriteBurgerListDTO>> GetAllFavoriteBurgersAsync(string userId);
    }
}
