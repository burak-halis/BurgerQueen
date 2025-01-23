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
    public class MenuSideItemService : BaseService<MenuSideItem>, IMenuSideItemService
    {
        public MenuSideItemService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<MenuSideItemListDTO>> GetSideItemsForMenuAsync(int menuId)
        {
            var menuSideItems = await base.GetByDTO(msi => msi.MenuId == menuId);

            return menuSideItems.Select(msi => new MenuSideItemListDTO
            {
                Id = msi.Id,
                MenuId = msi.MenuId,
                SideItemId = msi.SideItemId
            });
        }
    }
}
