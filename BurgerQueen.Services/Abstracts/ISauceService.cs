using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface ISauceService : IBaseService<Sauce>
    {
        Task AddSauceDTO(SauceAddDTO dto);
        Task UpdateSauceDTO(SauceUpdateDTO dto);
        Task DeleteSauceDTO(int id);
        Task<SauceUpdateDTO> GetSauceById(int id);
        Task<List<SauceListDTO>> GetSaucesAll();
    }
}
