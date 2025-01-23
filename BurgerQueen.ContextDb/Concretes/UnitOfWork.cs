using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.ContextDb.Concretes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEFContext _context;
        private readonly Dictionary<string, object> _repositoryDictionary = new Dictionary<string, object>();


        public UnitOfWork(IEFContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var entityName = typeof(TEntity).Name;
            if (!_repositoryDictionary.TryGetValue(entityName, out var repository))
            {
                repository = new BaseRepository<TEntity>(_context);
                _repositoryDictionary[entityName] = repository;
            }
            return (IBaseRepository<TEntity>)repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
