using System;
using System.Collections.Generic;
using System.Text;
using DevGuild.AspNetCore.Services.Data;
using DevGuild.AspNetCore.Services.ModelMapping;
using DevGuild.AspNetCore.Services.Permissions;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud
{
    /// <summary>
    /// Represents implementation of the entity controller services.
    /// </summary>
    /// <seealso cref="IEntityControllerServices" />
    public class EntityControllerServices : IEntityControllerServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityControllerServices"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="permissionsHub">The permissions hub.</param>
        /// <param name="mappingManager">The mapping manager.</param>
        public EntityControllerServices(IServiceProvider serviceProvider, IRepository repository, IPermissionsHub permissionsHub, IViewModelMappingManager mappingManager)
        {
            this.ServiceProvider = serviceProvider;
            this.Repository = repository;
            this.PermissionsHub = permissionsHub;
            this.MappingManager = mappingManager;
        }

        /// <inheritdoc />
        public IServiceProvider ServiceProvider { get; }

        /// <inheritdoc />
        public IRepository Repository { get; }

        /// <inheritdoc />
        public IPermissionsHub PermissionsHub { get; }

        /// <inheritdoc />
        public IViewModelMappingManager MappingManager { get; }
    }
}
