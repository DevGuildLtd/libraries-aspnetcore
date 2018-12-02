using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    /// <summary>
    /// Represents EntityFramework-based implementation of the <see cref="IEntityStore{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <seealso cref="IEntityStore{T}" />
    public class EntityFrameworkStore<T> : IEntityStore<T>
        where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkStore{T}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public EntityFrameworkStore(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        /// <inheritdoc />
        public IQueryable<T> Query()
        {
            return this.dbSet;
        }

        /// <inheritdoc />
        public IQueryable<T> Query(params String[] includes)
        {
            IQueryable<T> query = this.dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        /// <inheritdoc />
        public IQueryable<T> QueryWithoutTracking()
        {
            return this.dbSet.AsNoTracking();
        }

        /// <inheritdoc />
        public IQueryable<T> QueryWithoutTracking(params String[] includes)
        {
            var query = this.dbSet.AsNoTracking();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        /// <inheritdoc />
        public Task InsertAsync(T entity)
        {
            var entry = this.context.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    this.dbSet.Add(entity);
                    return Task.FromResult(0);
                default:
                    throw new InvalidOperationException("Entity is in invalid state");
            }
        }

        /// <inheritdoc />
        public Task UpdateAsync(T entity)
        {
            var entry = this.context.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Modified:
                    return Task.FromResult(0);
                case EntityState.Detached:
                case EntityState.Unchanged:
                    entry.State = EntityState.Modified;
                    return Task.FromResult(0);
                default:
                    throw new InvalidOperationException("Entity is in invalid state");
            }
        }

        /// <inheritdoc />
        public Task DeleteAsync(T entity)
        {
            this.dbSet.Remove(entity);
            return Task.FromResult(0);
        }
    }
}
