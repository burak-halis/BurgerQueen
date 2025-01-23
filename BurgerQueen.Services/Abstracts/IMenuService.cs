using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IMenuService : IBaseService<Menu>
    {
        Task AddMenuDTO(MenuAddDTO dto);
        Task UpdateMenuDTO(MenuUpdateDTO dto);
        Task DeleteMenuDTO(int id);
        Task<MenuUpdateDTO> GetMenuById(int id);
        Task<List<MenuListDTO>> GetMenusAll();
    }
}
