using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IDrinkService : IBaseService<Drink>
    {
        Task AddDrinkDTO(DrinkAddDTO dto);
        Task UpdateDrinkDTO(DrinkUpdateDTO dto);
        Task DeleteDrinkDTO(int id);
        Task<DrinkUpdateDTO> GetDrinkById(int id);
        Task<List<DrinkListDTO>> GetDrinksAll();
    }
}
