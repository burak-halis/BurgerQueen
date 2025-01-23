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
    public class MenuDrinkService : BaseService<MenuDrink>, IMenuDrinkService
    {
        public MenuDrinkService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<MenuDrinkListDTO>> GetDrinksForMenuAsync(int menuId)
        {
            var menuDrinks = await base.GetByDTO(md => md.MenuId == menuId);

            return menuDrinks.Select(md => new MenuDrinkListDTO
            {
                Id = md.Id,
                MenuId = md.MenuId,
                DrinkId = md.DrinkId
            });
        }
    }
}
