using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerQueen.Mapping;
using BurgerQueen.Shared.Enums;
using Microsoft.Extensions.Configuration;

namespace BurgerQueen.ContextDb.Concretes
{
    public class BaseContext : IdentityDbContext<ApplicationUser, IdentityRole, string>, IEFContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Burada, entity'lerin haritalanması yapılır.
            builder.ApplyConfiguration(new ApplicationUserMap());
            builder.ApplyConfiguration(new BurgerMap());
            builder.ApplyConfiguration(new BurgerExtraMap());
            builder.ApplyConfiguration(new DrinkMap());
            builder.ApplyConfiguration(new ExtraIngredientMap());
            builder.ApplyConfiguration(new FryMap());
            builder.ApplyConfiguration(new MenuMap());
            builder.ApplyConfiguration(new MenuBurgerMap());
            builder.ApplyConfiguration(new MenuDrinkMap());
            builder.ApplyConfiguration(new MenuFryMap());
            builder.ApplyConfiguration(new MenuSideItemMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new OrderItemMap());
            builder.ApplyConfiguration(new OrderStatusHistoryMap());
            builder.ApplyConfiguration(new SauceMap());
            builder.ApplyConfiguration(new SideItemMap());
            builder.ApplyConfiguration(new FavoriteBurgerMap());
            builder.ApplyConfiguration(new FavoriteDrinkMap());
            builder.ApplyConfiguration(new FavoriteFryMap());
            builder.ApplyConfiguration(new FavoriteMenuMap());
            builder.ApplyConfiguration(new FavoriteSideItemMap());
            builder.ApplyConfiguration(new FavoriteSauceMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        // Diğer metodlar...

        public override DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        EntityEntry IEFContext.Entry(object entry)
        {
            return base.Entry(entry);
        }

        EntityEntry<TEntity> IEFContext.Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry(entity);
        }

        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditEntities()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            var userId = "System"; // Gerçek kullanıcı kimliği alınabilir
            foreach (var entry in entries)
            {
                var baseEntity = (BaseEntity)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    baseEntity.CreatedDate = DateTime.UtcNow;
                    baseEntity.CreatedBy = userId;
                    if (baseEntity.Status == default)
                    {
                        baseEntity.Status = Status.Available;
                    }
                }

                baseEntity.ModifiedDate = DateTime.UtcNow;
                baseEntity.ModifiedBy = userId;
            }
        }

        IEnumerable<TEntity> IEFContext.SqlQuery<TEntity>(FormattableString query)
        {
            return Set<TEntity>().FromSqlInterpolated(query);
        }

        public IEnumerable<TEntity> SqlQuery<TEntity>(string query, params object[] parameters) where TEntity : class
        {
            return Set<TEntity>().FromSqlRaw(query, parameters);
        }

        public async Task<IEnumerable<TEntity>> SqlQueryAsync<TEntity>(string query, params object[] parameters) where TEntity : class
        {
            return await Set<TEntity>().FromSqlRaw(query, parameters).ToListAsync();
        }

        TResult IEFContext.SqlQuery<TResult>(string query, params object[] parameters) where TResult : class
        {
            return Set<TResult>().FromSqlRaw(query, parameters).AsEnumerable().FirstOrDefault();
        }

        public TResult SqlQueryRaw<TResult>(string query, params object[] parameters) where TResult : class
        {
            return Set<TResult>().FromSqlRaw(query, parameters).AsEnumerable().FirstOrDefault();
        }

        public async Task<TResult> SqlQueryRawAsync<TResult>(string query, params object[] parameters) where TResult : class
        {
            return await Set<TResult>().FromSqlRaw(query, parameters).FirstOrDefaultAsync();
        }
    }
}
