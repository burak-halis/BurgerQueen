using BurgerQueen.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task AddDTOAsync(T entityDTO);
        Task UpdateDTOAsync(T entityDTO);
        Task DeleteDTOAsync(int id);
        Task<List<T>> GetAllDTO();
        Task<T> GetByIdDTO(Expression<Func<T, bool>> exp);
        Task<List<T>> GetByDTO(Expression<Func<T, bool>> exp);
    }
}
