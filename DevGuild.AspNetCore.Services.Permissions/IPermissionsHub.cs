using System;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.Permissions
{
    /// <summary>
    /// Provides the ability to retrieve permissions manager for registered security scope.
    /// </summary>
    public interface IPermissionsHub
    {
        /// <summary>
        /// Gets the permissions manager registered for the provided security scope.
        /// </summary>
        /// <typeparam name="T">Type of the permissions manager</typeparam>
        /// <param name="path">The security scope path.</param>
        /// <returns>An instance of permissions manager casted to type <typeparamref name="T"/>.</returns>
        T GetManager<T>(String path)
            where T : ICorePermissionsManager;

        /// <summary>
        /// Asynchronously creates the unauthorized exception of the appropriate type.
        /// </summary>
        /// <returns>A task that represents the operation.</returns>
        Task<Exception> CreateUnauthorizedExceptionAsync();
    }
}
