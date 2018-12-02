using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Identity
{
    /// <summary>
    /// Defines interface of the principal user accessor service.
    /// </summary>
    public interface IPrincipalUserAccessorService
    {
        /// <summary>
        /// Asynchronously gets the principal user.
        /// </summary>
        /// <returns>A task that represents the operation which result is set to principal user.</returns>
        Task<ClaimsPrincipal> GetPrincipalUserAsync();
    }
}
