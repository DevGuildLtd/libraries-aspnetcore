using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Permissions;
using DevGuild.AspNetCore.Services.Permissions.Entity;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud
{
    /// <summary>
    /// Represents default implementation of hierarchical entity permissions validator.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="DefaultEntityPermissionsValidator{TEntity}" />
    /// <seealso cref="IHierarchicalEntityPermissionsValidator{TEntity}" />
    public class DefaultHierarchicalEntityPermissionsValidator<TEntity> : DefaultEntityPermissionsValidator<TEntity>, IHierarchicalEntityPermissionsValidator<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultHierarchicalEntityPermissionsValidator{TEntity}"/> class.
        /// </summary>
        /// <param name="permissionsHub">The permissions hub.</param>
        /// <param name="typeManagerPath">The type permissions manager path.</param>
        /// <param name="entityManagerPath">The entity permissions manager path.</param>
        /// <param name="propertyManagerPath">The property permissions manager path.</param>
        public DefaultHierarchicalEntityPermissionsValidator(
            IPermissionsHub permissionsHub,
            String typeManagerPath,
            String entityManagerPath,
            String propertyManagerPath)
            : base(permissionsHub, typeManagerPath, entityManagerPath, propertyManagerPath)
        {
        }

        /// <inheritdoc />
        public async Task<Boolean> CanCreateChildAsync(TEntity entity)
        {
            var canAccess = this.TypePermissionsManager == null || await this.TypePermissionsManager.CheckPermissionAsync(EntityPermissions.EntityType.Access) == PermissionsResult.Allow;
            var canCreateChild = this.EntityPermissionsManager == null || await this.EntityPermissionsManager.CheckPermissionAsync(entity, EntityPermissions.HierarchicalEntity.CreateChild) == PermissionsResult.Allow;
            return canAccess && canCreateChild;
        }

        /// <inheritdoc />
        public async Task DemandCanCreateChildAsync(TEntity entity)
        {
            await this.TypePermissionsManager.DemandPermissionOrDefaultAsync(EntityPermissions.EntityType.Access);
            await this.EntityPermissionsManager.DemandPermissionOrDefaultAsync(entity, EntityPermissions.HierarchicalEntity.CreateChild);
        }
    }
}
