using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Defines interface of the authenticated user id accessor service.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IAuthenticatedUserIdAccessorService<TKey>
    {
        /// <summary>
        /// Asynchronously gets the current user identifier.
        /// </summary>
        /// <returns>A task that represents the operation and contains user identifier as a result.</returns>
        Task<TKey> GetUserIdAsync();
    }
}
