using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.Entity;
using BurgerQueen.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.ContextDb.Concretes
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IEFContext _context;
        private readonly DbSet<T> _table;

        public BaseRepository(IEFContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await _table.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            entity.Status = Status.SoftDeleted;
            await UpdateAsync(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public async Task<List<T>> GetAllActive()
        {
            return await _table.Where(x => x.Status == Status.Available).ToListAsync();
        }

        public async Task<List<T>> GetBy(Expression<Func<T, bool>> exp)
        {
            return await _table.Where(exp).ToListAsync();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> exp)
        {
            return await _table.FirstOrDefaultAsync(exp);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp)
        {
            return await _table.FirstOrDefaultAsync(exp);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> exp)
        {
            return await _table.AnyAsync(exp);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> exp = null)
        {
            return await (exp != null ? _table.CountAsync(exp) : _table.CountAsync());
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> exp)
        {
            return await _table.SingleOrDefaultAsync(exp);
        }

        public async Task<List<T>> GetLatest(int count)
        {
            return await _table.OrderByDescending(e => e.CreatedDate).Take(count).ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
