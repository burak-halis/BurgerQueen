using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IBurgerService : IBaseService<Burger>
    {
        Task AddBurgerDTO(BurgerAddDTO dto);
        Task UpdateBurgerDTO(BurgerUpdateDTO dto);
        Task DeleteBurgerDTO(int id);
        Task<BurgerUpdateDTO> GetBurgerById(int id);
        Task<List<BurgerListDTO>> GetBurgersAll();
    }
}
