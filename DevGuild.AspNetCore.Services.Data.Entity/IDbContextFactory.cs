using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    /// <summary>
    /// Defines interface of the database context factory.
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// Creates the database context.
        /// </summary>
        /// <returns>Created database context.</returns>
        DbContext CreateDbContext();
    }
}
