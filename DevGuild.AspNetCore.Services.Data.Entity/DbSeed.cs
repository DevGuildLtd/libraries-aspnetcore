using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevGuild.AspNetCore.Services.Data.Entity
{
    /// <summary>
    /// Represents a base class used for database seeding.
    /// </summary>
    /// <typeparam name="TContext">The type of the database context.</typeparam>
    /// <seealso cref="System.IDisposable" />
    public abstract class DbSeed<TContext> : IDisposable
        where TContext : DbContext
    {
        private readonly IServiceProvider serviceProvider;
        private readonly TContext databaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbSeed{TContext}" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        protected DbSeed(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.databaseContext = this.serviceProvider.GetService<TContext>();

            this.Context = new DbSeedContext<TContext>(this.serviceProvider, this.databaseContext);
        }

        /// <summary>
        /// Asynchronously seeds the database.
        /// </summary>
        /// <returns>A task that represents seeding operation.</returns>
        public abstract Task SeedAsync();

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        protected DbSeedContext<TContext> Context { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// When overriden in a derived class, performs freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">if set to <c>true</c> managed resources should be disposed.</param>
        protected virtual void Dispose(Boolean disposing)
        {
        }
    }
}
