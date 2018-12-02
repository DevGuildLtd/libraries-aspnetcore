using System;
using DevGuild.AspNetCore.Services.Data;
using DevGuild.AspNetCore.Services.ModelMapping;
using DevGuild.AspNetCore.Services.Permissions;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud
{
    /// <summary>
    /// Defines interface for common entity controller services.
    /// </summary>
    public interface IEntityControllerServices
    {
        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>
        /// The service provider.
        /// </value>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        IRepository Repository { get; }

        /// <summary>
        /// Gets the permissions hub.
        /// </summary>
        /// <value>
        /// The permissions hub.
        /// </value>
        IPermissionsHub PermissionsHub { get; }

        /// <summary>
        /// Gets the mapping manager.
        /// </summary>
        /// <value>
        /// The mapping manager.
        /// </value>
        IViewModelMappingManager MappingManager { get; }
    }
}
