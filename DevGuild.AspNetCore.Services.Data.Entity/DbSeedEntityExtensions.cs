using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    /// <summary>
    /// Contains extensions for database seed.
    /// </summary>
    public static class DbSeedEntityExtensions
    {
        /// <summary>
        /// Asynchronously seeds the entity to the database.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="context">The database seed context.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Either existing or created entity.</returns>
        public static async Task<TEntity> SeedEntityAsync<TContext, TEntity>(this DbSeedContext<TContext> context, TEntity entity)
            where TContext : DbContext
            where TEntity : class
        {
            var equalExpression = context.GetKeyEqualityExpression(entity);
            var existing = await context.Set<TEntity>().SingleOrDefaultAsync(equalExpression);
            if (existing != null)
            {
                return existing;
            }

            context.Set<TEntity>().Add(entity);
            return entity;
        }

        /// <summary>
        /// Asynchronously seeds the entity to the database.
        /// </summary>
        /// <typeparam name="TContext">The type of the context.</typeparam>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the custom key.</typeparam>
        /// <param name="context">The database seed context.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="customKeyExpression">The custom key expression.</param>
        /// <returns>Either existing or created entity.</returns>
        public static async Task<TEntity> SeedEntityAsync<TContext, TEntity, TKey>(this DbSeedContext<TContext> context, TEntity entity, Expression<Func<TEntity, TKey>> customKeyExpression)
            where TContext : DbContext
            where TEntity : class
        {
            var equalExpression = context.GetCustomEqualityExpression(entity, customKeyExpression);
            var existing = await context.Set<TEntity>().SingleOrDefaultAsync(equalExpression);
            if (existing != null)
            {
                return existing;
            }

            context.Set<TEntity>().Add(entity);
            return entity;
        }
    }

}
