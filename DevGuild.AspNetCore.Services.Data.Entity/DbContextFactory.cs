using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    /// <summary>
    /// Represents a database context factory.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="DevGuild.AspNetCore.Services.Data.Entity.IDbContextFactory{TContext}" />
    public class DbContextFactory<TContext> : IDbContextFactory<TContext>
        where TContext : DbContext
    {
        private readonly Func<TContext> constructor;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextFactory{TContext}"/> class.
        /// </summary>
        /// <param name="constructor">The context constructor.</param>
        public DbContextFactory(Func<TContext> constructor)
        {
            this.constructor = constructor;
        }

        /// <inheritdoc />
        public TContext CreateDbContext()
        {
            return this.constructor();
        }

        /// <inheritdoc />
        DbContext IDbContextFactory.CreateDbContext()
        {
            return this.CreateDbContext();
        }
    }

}
