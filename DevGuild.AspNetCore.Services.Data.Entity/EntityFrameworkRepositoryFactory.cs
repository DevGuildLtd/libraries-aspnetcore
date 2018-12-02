using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    /// <summary>
    /// Represents EntityFramework-based repository factory implementation.
    /// </summary>
    /// <seealso cref="IRepositoryFactory" />
    public class EntityFrameworkRepositoryFactory : IRepositoryFactory
    {
        private readonly IDbContextFactory contextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrameworkRepositoryFactory"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public EntityFrameworkRepositoryFactory(IDbContextFactory contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        /// <inheritdoc />
        public IRepository CreateRepository()
        {
            return new EntityFrameworkRepository(this.contextFactory.CreateDbContext());
        }
    }
}
