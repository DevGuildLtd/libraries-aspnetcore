using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Permissions;
using DevGuild.AspNetCore.Services.Permissions.Entity;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud
{
    /// <summary>
    /// Represents default implementation of the entity permissions validator.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IEntityPermissionsValidator{TEntity}" />
    public class DefaultEntityPermissionsValidator<TEntity> : IEntityPermissionsValidator<TEntity>
    {
        private readonly IPermissionsHub permissionsHub;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultEntityPermissionsValidator{TEntity}"/> class.
        /// </summary>
        /// <param name="permissionsHub">The permissions hub.</param>
        /// <param name="typeManagerPath">The type permissions manager path.</param>
        /// <param name="entityManagerPath">The entity permissions manager path.</param>
        /// <param name="propertyManagerPath">The property permissions manager path.</param>
        public DefaultEntityPermissionsValidator(
            IPermissionsHub permissionsHub,
            String typeManagerPath,
            String entityManagerPath,
            String propertyManagerPath)
        {
            this.permissionsHub = permissionsHub;

            this.TypePermissionsManager = typeManagerPath != null ? permissionsHub.GetManager<IPermissionsManager>(typeManagerPath) : null;
            this.EntityPermissionsManager = entityManagerPath != null ? permissionsHub.GetManager<IPermissionsManager<TEntity>>(entityManagerPath) : null;
            this.PropertyPermissionsManager = propertyManagerPath != null ? permissionsHub.GetManager<IPermissionsManager<String>>(propertyManagerPath) : null;
        }

        /// <summary>
        /// Gets the type permissions manager.
        /// </summary>
        /// <value>
        /// The type permissions manager.
        /// </value>
        protected IPermissionsManager TypePermissionsManager { get; }

        /// <summary>
        /// Gets the entity permissions manager.
        /// </summary>
        /// <value>
        /// The entity permissions manager.
        /// </value>
        protected IPermissionsManager<TEntity> EntityPermissionsManager { get; }

        /// <summary>
        /// Gets the property permissions manager.
        /// </summary>
        /// <value>
        /// The property permissions manager.
        /// </value>
        protected IPermissionsManager<String> PropertyPermissionsManager { get; }

        /// <inheritdoc />
        public async Task<Boolean> CanIndexAsync()
        {
            var canAccess = this.TypePermissionsManager == null || await this.TypePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            return canAccess;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanCreateAsync()
        {
            var canAccess = this.TypePermissionsManager == null || await this.TypePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            var canCreate = this.TypePermissionsManager == null || await this.TypePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Create) == PermissionsResult.Allow;
            return canAccess && canCreate;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanDetailsAsync(TEntity entity)
        {
            var canAccess = this.TypePermissionsManager == null || await this.TypePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            var canRead = this.EntityPermissionsManager == null || await this.EntityPermissionsManager.CheckPermissionAsync(entity, EntityPermissions.Entity.Read) == PermissionsResult.Allow;
            return canAccess && canRead;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanEditAsync(TEntity entity)
        {
            var canAccess = this.TypePermissionsManager == null || await this.TypePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            var canUpdate = this.EntityPermissionsManager == null || await this.EntityPermissionsManager.CheckPermissionAsync(entity, EntityPermissions.Entity.Update) == PermissionsResult.Allow;
            return canAccess && canUpdate;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanDeleteAsync(TEntity entity)
        {
            var canAccess = this.TypePermissionsManager == null || await this.TypePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            var canDelete = this.EntityPermissionsManager == null || await this.EntityPermissionsManager.CheckPermissionAsync(entity, EntityPermissions.Entity.Delete) == PermissionsResult.Allow;
            return canAccess && canDelete;
        }

        /// <inheritdoc />
        public async Task<IQueryable<TEntity>> RequireReadAccessAsync(IQueryable<TEntity> query)
        {
            return await this.EntityPermissionsManager.ApplyQueryFilterOrDefaultAsync(query, EntityPermissions.Entity.Read);
        }

        /// <inheritdoc />
        public async Task DemandCanIndexAsync()
        {
            await this.TypePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
        }

        /// <inheritdoc />
        public async Task DemandCanCreateAsync()
        {
            await this.TypePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
            await this.TypePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Create);
        }

        /// <inheritdoc />
        public async Task DemandCanDetailsAsync(TEntity entity)
        {
            await this.TypePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
            await this.EntityPermissionsManager.DemandPermissionOrDefaultAsync(entity, EntityPermissions.Entity.Read);
        }

        /// <inheritdoc />
        public async Task DemandCanEditAsync(TEntity entity)
        {
            await this.TypePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
            await this.EntityPermissionsManager.DemandPermissionOrDefaultAsync(entity, EntityPermissions.Entity.Update);
        }

        /// <inheritdoc />
        public async Task DemandCanDeleteAsync(TEntity entity)
        {
            await this.TypePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
            await this.EntityPermissionsManager.DemandPermissionOrDefaultAsync(entity, EntityPermissions.Entity.Delete);
        }

        /// <inheritdoc />
        public async Task<Boolean> CanReadPropertyAsync(String propertyName)
        {
            return this.PropertyPermissionsManager == null || await this.PropertyPermissionsManager.CheckPermissionAsync(propertyName, EntityPermissions.EntityProperty.Read) == PermissionsResult.Allow;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanInitializePropertyAsync(String propertyName)
        {
            return this.PropertyPermissionsManager == null || await this.PropertyPermissionsManager.CheckPermissionAsync(propertyName, EntityPermissions.EntityProperty.Initialize) == PermissionsResult.Allow;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanUpdatePropertyAsync(String propertyName)
        {
            return this.PropertyPermissionsManager == null || await this.PropertyPermissionsManager.CheckPermissionAsync(propertyName, EntityPermissions.EntityProperty.Update) == PermissionsResult.Allow;
        }
    }
}
