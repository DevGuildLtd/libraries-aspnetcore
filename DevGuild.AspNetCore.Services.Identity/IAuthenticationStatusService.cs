using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Defines interface of the authentication status service.
    /// </summary>
    public interface IAuthenticationStatusService
    {
        /// <summary>
        /// Asynchronously gets the authentication status.
        /// </summary>
        /// <returns>A task that represents the operation which result is set to <c>true</c> if current user is authenticated.</returns>
        Task<Boolean> GetAuthenticationStatusAsync();
    }
}
