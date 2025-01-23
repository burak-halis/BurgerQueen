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
    public class MenuBurgerService : BaseService<MenuBurger>, IMenuBurgerService
    {
        public MenuBurgerService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<MenuBurgerListDTO>> GetBurgersForMenuAsync(int menuId)
        {
            var menuBurgers = await base.GetByDTO(mb => mb.MenuId == menuId);

            return menuBurgers.Select(mb => new MenuBurgerListDTO
            {
                Id = mb.Id,
                MenuId = mb.MenuId,
                BurgerId = mb.BurgerId
            });
        }
    }
}
