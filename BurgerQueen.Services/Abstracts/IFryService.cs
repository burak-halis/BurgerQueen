using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IFryService : IBaseService<Fry>
    {
        Task AddFryDTO(FryAddDTO dto);
        Task UpdateFryDTO(FryUpdateDTO dto);
        Task DeleteFryDTO(int id);
        Task<FryUpdateDTO> GetFryById(int id);
        Task<List<FryListDTO>> GetFriesAll();
    }
}
