using BurgerQueen.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.ContextDb.Abstracts
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAll();
        Task<List<T>> GetAllActive();
        Task<List<T>> GetBy(Expression<Func<T, bool>> exp);
        Task<T> GetById(Expression<Func<T, bool>> exp);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp);
        Task<bool> AnyAsync(Expression<Func<T, bool>> exp);
        Task<int> CountAsync(Expression<Func<T, bool>> exp = null);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> exp);
        Task<List<T>> GetLatest(int count);
        Task<int> SaveChangesAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> exp);
        Task<List<T>> GetByAsync(Expression<Func<T, bool>> exp);



    }
}
