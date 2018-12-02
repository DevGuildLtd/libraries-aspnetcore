using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    /// <summary>
    /// Represents EntityFramework-based repository implementation.
    /// </summary>
    /// <seealso cref="IRepository" />
    public sealed class EntityFrameworkRepository : IRepository
    {
        private readonly DbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EntityFrameworkRepository(DbContext context)
        {
            this.context = context;
        }

        /// <inheritdoc />
        public IEntityStore<T> GetEntityStore<T>()
            where T : class
        {
            return new EntityFrameworkStore<T>(this.context);
        }

        /// <inheritdoc />
        public Task<Int32> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.context?.Dispose();
        }
    }
}
