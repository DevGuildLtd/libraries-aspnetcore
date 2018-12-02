using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    /// <summary>
    /// Defines interface of the database context factory.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <seealso cref="DevGuild.AspNetCore.Services.Data.Entity.IDbContextFactory" />
    public interface IDbContextFactory<out TContext> : IDbContextFactory
        where TContext : DbContext
    {
        /// <summary>
        /// Creates the database context.
        /// </summary>
        /// <returns>Created database context.</returns>
        new TContext CreateDbContext();
    }
}
