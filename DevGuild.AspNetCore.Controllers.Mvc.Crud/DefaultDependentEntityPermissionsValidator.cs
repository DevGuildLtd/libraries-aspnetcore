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
    /// Represents default implementation of dependent entity permissions validator.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TParentEntity">The type of the parent entity.</typeparam>
    /// <seealso cref="IDependentEntityPermissionsValidator{TEntity, TParentEntity}" />
    public class DefaultDependentEntityPermissionsValidator<TEntity, TParentEntity> : IDependentEntityPermissionsValidator<TEntity, TParentEntity>
    {
        private readonly IPermissionsHub permissionsHub;
        private readonly IPermissionsManager typePermissionsManager;
        private readonly IPermissionsManager<TEntity> entityPermissionsManager;
        private readonly IPermissionsManager<String> propertyPermissionsManager;
        private readonly IPermissionsManager<TParentEntity> parentEntityPermissionsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDependentEntityPermissionsValidator{TEntity, TParentEntity}"/> class.
        /// </summary>
        /// <param name="permissionsHub">The permissions hub.</param>
        /// <param name="typeManagerPath">The type permissions manager path.</param>
        /// <param name="entityManagerPath">The entity permissions manager path.</param>
        /// <param name="propertyManagerPath">The property permissions manager path.</param>
        /// <param name="parentEntityManagerPath">The parent entity permissions manager path.</param>
        public DefaultDependentEntityPermissionsValidator(
            IPermissionsHub permissionsHub,
            String typeManagerPath,
            String entityManagerPath,
            String propertyManagerPath,
            String parentEntityManagerPath)
        {
            this.permissionsHub = permissionsHub;

            this.typePermissionsManager = typeManagerPath != null ? permissionsHub.GetManager<IPermissionsManager>(typeManagerPath) : null;
            this.entityPermissionsManager = entityManagerPath != null ? permissionsHub.GetManager<IPermissionsManager<TEntity>>(entityManagerPath) : null;
            this.propertyPermissionsManager = propertyManagerPath != null ? permissionsHub.GetManager<IPermissionsManager<String>>(propertyManagerPath) : null;
            this.parentEntityPermissionsManager = parentEntityManagerPath != null ? permissionsHub.GetManager<IPermissionsManager<TParentEntity>>(parentEntityManagerPath) : null;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanIndexAsync(TParentEntity parentEntity)
        {
            var canAccess = this.typePermissionsManager == null || await this.typePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            var canReadParent = this.parentEntityPermissionsManager == null || await this.parentEntityPermissionsManager.CheckPermissionAsync(parentEntity, EntityPermissions.ParentEntity.Read) == PermissionsResult.Allow;
            return canAccess && canReadParent;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanCreateAsync(TParentEntity parentEntity)
        {
            var canAccess = this.typePermissionsManager == null || await this.typePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            var canCreate = this.typePermissionsManager == null || await this.typePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Create) == PermissionsResult.Allow;
            var canReadParent = this.parentEntityPermissionsManager == null || await this.parentEntityPermissionsManager.CheckPermissionAsync(parentEntity, EntityPermissions.ParentEntity.Read) == PermissionsResult.Allow;
            var canCreateDependent = this.parentEntityPermissionsManager == null || await this.parentEntityPermissionsManager.CheckPermissionAsync(parentEntity, EntityPermissions.ParentEntity.CreateDependent) == PermissionsResult.Allow;
            return canAccess && canCreate && canReadParent && canCreateDependent;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanDetailsAsync(TEntity entity)
        {
            var canAccess = this.typePermissionsManager == null || await this.typePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            var canRead = this.entityPermissionsManager == null || await this.entityPermissionsManager.CheckPermissionAsync(entity, EntityPermissions.Entity.Read) == PermissionsResult.Allow;
            return canAccess && canRead;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanEditAsync(TEntity entity)
        {
            var canAccess = this.typePermissionsManager == null || await this.typePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            var canEdit = this.entityPermissionsManager == null || await this.entityPermissionsManager.CheckPermissionAsync(entity, EntityPermissions.Entity.Update) == PermissionsResult.Allow;
            return canAccess && canEdit;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanDeleteAsync(TEntity entity)
        {
            var canAccess = this.typePermissionsManager == null || await this.typePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            var canDelete = this.entityPermissionsManager == null || await this.entityPermissionsManager.CheckPermissionAsync(entity, EntityPermissions.Entity.Delete) == PermissionsResult.Allow;
            return canAccess && canDelete;
        }

        /// <inheritdoc />
        public async Task<IQueryable<TEntity>> RequireReadAccessAsync(TParentEntity parent, IQueryable<TEntity> query)
        {
            return await this.entityPermissionsManager.ApplyQueryFilterOrDefaultAsync(query, EntityPermissions.Entity.Read);
        }

        /// <inheritdoc />
        public async Task DemandCanIndexAsync(TParentEntity parentEntity)
        {
            await this.typePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
            await this.parentEntityPermissionsManager.DemandPermissionOrDefaultAsync(parentEntity, EntityPermissions.ParentEntity.Read);
        }

        /// <inheritdoc />
        public async Task DemandCanCreateAsync(TParentEntity parentEntity)
        {
            await this.typePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
            await this.typePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Create);
            await this.parentEntityPermissionsManager.DemandPermissionOrDefaultAsync(parentEntity, EntityPermissions.ParentEntity.Read);
            await this.parentEntityPermissionsManager.DemandPermissionOrDefaultAsync(parentEntity, EntityPermissions.ParentEntity.CreateDependent);
        }

        /// <inheritdoc />
        public async Task DemandCanDetailsAsync(TEntity entity)
        {
            await this.typePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
            await this.entityPermissionsManager.DemandPermissionOrDefaultAsync(entity, EntityPermissions.Entity.Read);
        }

        /// <inheritdoc />
        public async Task DemandCanEditAsync(TEntity entity)
        {
            await this.typePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
            await this.entityPermissionsManager.DemandPermissionOrDefaultAsync(entity, EntityPermissions.Entity.Update);
        }

        /// <inheritdoc />
        public async Task DemandCanDeleteAsync(TEntity entity)
        {
            await this.typePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
            await this.entityPermissionsManager.DemandPermissionOrDefaultAsync(entity, EntityPermissions.Entity.Update);
        }

        /// <inheritdoc />
        public async Task<Boolean> CanReadPropertyAsync(String propertyName)
        {
            return this.propertyPermissionsManager == null || await this.propertyPermissionsManager.CheckPermissionAsync(propertyName, EntityPermissions.EntityProperty.Read) == PermissionsResult.Allow;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanInitializePropertyAsync(String propertyName)
        {
            return this.propertyPermissionsManager == null || await this.propertyPermissionsManager.CheckPermissionAsync(propertyName, EntityPermissions.EntityProperty.Initialize) == PermissionsResult.Allow;
        }

        /// <inheritdoc />
        public async Task<Boolean> CanUpdatePropertyAsync(String propertyName)
        {
            return this.propertyPermissionsManager == null || await this.propertyPermissionsManager.CheckPermissionAsync(propertyName, EntityPermissions.EntityProperty.Update) == PermissionsResult.Allow;
        }
    }
}
