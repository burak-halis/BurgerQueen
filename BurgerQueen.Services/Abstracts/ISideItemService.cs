using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface ISideItemService : IBaseService<SideItem>
    {
        Task AddSideItemDTO(SideItemAddDTO dto);
        Task UpdateSideItemDTO(SideItemUpdateDTO dto);
        Task DeleteSideItemDTO(int id);
        Task<SideItemUpdateDTO> GetSideItemById(int id);
        Task<List<SideItemListDTO>> GetSideItemsAll();
    }

}
