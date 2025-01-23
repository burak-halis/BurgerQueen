using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Concretes
{
    public class MenuService : BaseService<Menu>, IMenuService
    {
        public MenuService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddMenuDTO(MenuAddDTO dto)
        {
            var menu = new Menu
            {
                Name = dto.Name,
                Description = dto.Description,
                TotalPrice = dto.TotalPrice,
                ImagePath = dto.ImagePath,
                MenuType = dto.MenuType
            };
            await base.AddDTOAsync(menu);
        }

        public async Task UpdateMenuDTO(MenuUpdateDTO dto)
        {
            try
            {
                var menu = await base.GetByIdDTO(m => m.Id == dto.Id);
                if (menu != null)
                {
                    menu.Name = dto.Name;
                    menu.Description = dto.Description;
                    menu.TotalPrice = dto.TotalPrice;
                    menu.ImagePath = dto.ImagePath;
                    menu.MenuType = dto.MenuType;

                    await base.UpdateDTOAsync(menu);
                }
                else
                {
                    throw new Exception("Menu bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncelleme işleminde bir hata var: " + ex.Message);
            }
        }

        public async Task DeleteMenuDTO(int id)
        {
            await base.DeleteDTOAsync(id);
        }

        public async Task<MenuUpdateDTO> GetMenuById(int id)
        {
            var menu = await base.GetByIdDTO(m => m.Id == id);
            if (menu != null)
            {
                return new MenuUpdateDTO
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    Description = menu.Description,
                    TotalPrice = menu.TotalPrice,
                    ImagePath = menu.ImagePath,
                    MenuType = menu.MenuType
                };
            }
            return null;
        }

        public async Task<List<MenuListDTO>> GetMenusAll()
        {
            var menus = await base.GetAllDTO();
            return menus.Select(m => new MenuListDTO
            {
                Id = m.Id,
                Name = m.Name,
                TotalPrice = m.TotalPrice,
                ImagePath = m.ImagePath
            }).ToList();
        }
    }
}
