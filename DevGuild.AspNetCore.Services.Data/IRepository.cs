using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Data
{
    /// <summary>
    /// Defines interface of a repository.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Gets the entity store.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <returns>An entity store.</returns>
        IEntityStore<T> GetEntityStore<T>()
            where T : class;

        /// <summary>
        /// Asynchronously saves the changes to the repository.
        /// </summary>
        /// <returns>A task tha represents save operation.</returns>
        Task<Int32> SaveChangesAsync();
    }
}
