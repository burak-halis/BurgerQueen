using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IFavoriteFryService : IBaseService<FavoriteFry>
    {
        Task AddFavoriteFryAsync(FavoriteFryAddDTO dto);
        Task UpdateFavoriteFryAsync(FavoriteFryUpdateDTO dto);
        Task DeleteFavoriteFryAsync(int friesId, string userId);
        Task<FavoriteFryUpdateDTO> GetFavoriteFryByIdAsync(int friesId, string userId);
        Task<List<FavoriteFryListDTO>> GetAllFavoriteFriesAsync(string userId);
    }
}
