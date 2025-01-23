using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IExtraIngredientService : IBaseService<ExtraIngredient>
    {
        Task AddExtraIngredientDTO(ExtraIngredientAddDTO dto);
        Task UpdateExtraIngredientDTO(ExtraIngredientUpdateDTO dto);
        Task DeleteExtraIngredientDTO(int id);
        Task<ExtraIngredientUpdateDTO> GetExtraIngredientById(int id);
        Task<List<ExtraIngredientListDTO>> GetExtraIngredientsAll();
    }
}
