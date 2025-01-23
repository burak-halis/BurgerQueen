using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.ContextDb.Abstracts
{
    public interface IEFContext : IDisposable
    {
        EntityEntry Entry(object entry);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        // SQL sorguları için metodlar. Bu metodlar, doğrudan SQL sorguları çalıştırmak için kullanılabilir.
        IEnumerable<TEntity> SqlQuery<TEntity>(FormattableString query) where TEntity : class;
        TResult SqlQuery<TResult>(string query, params object[] parameters) where TResult : class;
    }
}
