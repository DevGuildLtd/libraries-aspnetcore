using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Defines interface for authenticate user accessor service.
    /// </summary>
    /// <typeparam name="TUser">The type of the user.</typeparam>
    public interface IAuthenticatedUserAccessorService<TUser>
    {
        /// <summary>
        /// Asynchronously gets the current user.
        /// </summary>
        /// <returns>A task that represents the operation which result is set to the current user.</returns>
        Task<TUser> GetUserAsync();
    }
}
