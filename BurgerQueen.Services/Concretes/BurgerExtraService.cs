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
    public class BurgerExtraService : BaseService<BurgerExtra>, IBurgerExtraService
    {
        public BurgerExtraService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<BurgerExtraListDTO>> GetExtrasForBurgerAsync(int burgerId)
        {
            // BaseService'ten gelen GetByDTO metodunu kullanarak BurgerExtra listesini al
            var burgerExtras = await base.GetByDTO(be => be.BurgerId == burgerId);

            // Bu listeyi BurgerExtraListDTO listesine dönüştür
            return burgerExtras.Select(be => new BurgerExtraListDTO
            {
                Id = be.Id,
                BurgerId = be.BurgerId,
                ExtraIngredientId = be.ExtraIngredientId
            });
        }
    }
}
