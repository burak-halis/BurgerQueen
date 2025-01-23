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
    public class MenuFryService : BaseService<MenuFry>, IMenuFryService
    {
        public MenuFryService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<MenuFryListDTO>> GetFriesForMenuAsync(int menuId)
        {
            var menuFries = await base.GetByDTO(mf => mf.MenuId == menuId);

            return menuFries.Select(mf => new MenuFryListDTO
            {
                Id = mf.Id,
                MenuId = mf.MenuId,
                FryId = mf.FryId
            });
        }
    }
}
