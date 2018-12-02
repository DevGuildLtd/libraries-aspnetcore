using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Controllers.Mvc.ActionHandlers;
using DevGuild.AspNetCore.Services.Data;
using DevGuild.AspNetCore.Services.ModelMapping;
using DevGuild.AspNetCore.Services.Permissions.Entity;
using DevGuild.AspNetCore.Services.Permissions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents base dependent CRUD action handler.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TParentIdentifier">The type of the parent identifier.</typeparam>
    /// <typeparam name="TParentEntity">The type of the parent entity.</typeparam>
    /// <typeparam name="TOverrides">The type of the overrides.</typeparam>
    public abstract class BaseCrudDependentActionHandler<TIdentifier, TEntity, TParentIdentifier, TParentEntity, TOverrides> : BaseActionHandler<TOverrides>
        where TEntity : class
        where TParentEntity : class
        where TOverrides : BaseCrudDependentActionOverrides<TIdentifier, TEntity, TParentIdentifier, TParentEntity>
    {
        private readonly Lazy<IEntityStore<TEntity>> store;
        private readonly Lazy<IEntityStore<TParentEntity>> parentStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCrudDependentActionHandler{TIdentifier, TEntity, TParentIdentifier, TParentEntity, TOverrides}"/> class.
        /// </summary>
        /// <param name="controller">The containing controller.</param>
        /// <param name="controllerServices">The controller services.</param>
        /// <param name="permissionsValidator">The permissions validator.</param>
        protected BaseCrudDependentActionHandler(Controller controller, IEntityControllerServices controllerServices, IDependentEntityPermissionsValidator<TEntity, TParentEntity> permissionsValidator)
            : base(controller)
        {
            this.ControllerServices = controllerServices;
            this.PermissionsValidator = permissionsValidator;

            this.store = new Lazy<IEntityStore<TEntity>>(() => this.Repository.GetEntityStore<TEntity>());
            this.parentStore = new Lazy<IEntityStore<TParentEntity>>(() => this.ControllerServices.Repository.GetEntityStore<TParentEntity>());
        }

        /// <summary>
        /// Gets the controller services.
        /// </summary>
        /// <value>
        /// The controller services.
        /// </value>
        protected IEntityControllerServices ControllerServices { get; }

        /// <summary>
        /// Gets the permissions validator.
        /// </summary>
        /// <value>
        /// The permissions validator.
        /// </value>
        protected IDependentEntityPermissionsValidator<TEntity, TParentEntity> PermissionsValidator { get; }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        protected IRepository Repository => this.ControllerServices.Repository;

        /// <summary>
        /// Gets the entity store.
        /// </summary>
        /// <value>
        /// The entity store.
        /// </value>
        protected IEntityStore<TEntity> Store => this.store.Value;

        /// <summary>
        /// Gets the parent entity store.
        /// </summary>
        /// <value>
        /// The parent entity store.
        /// </value>
        protected IEntityStore<TParentEntity> ParentStore => this.parentStore.Value;

        /// <summary>
        /// Gets the model identifier mapper.
        /// </summary>
        /// <value>
        /// The model identifier mapper.
        /// </value>
        protected IModelIdentifierMapper<TIdentifier, TEntity> ModelIdentifierMapper => this.ControllerServices.MappingManager.GetModelIdentifierMapper<TIdentifier, TEntity>();

        /// <summary>
        /// Asynchronously queries a single parent entity from the parent entity store using specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the parent entity.</param>
        /// <returns>A task that represents the operation and contains queried entity as a result.</returns>
        protected Task<TParentEntity> QuerySingleParentEntityAsync(TParentIdentifier id)
        {
            if (this.Overrides.QuerySingleParentEntity != null)
            {
                return this.Overrides.QuerySingleParentEntity(id);
            }

            async Task<TParentEntity> DefaultImplementation()
            {
                var expression = await this.BuildSingleParentQueryExpressionAsync(id);
                var entity = await this.ParentStore.Query().SingleOrDefaultAsync(expression);

                return entity;
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously builds the expression for single parent entity query.
        /// </summary>
        /// <param name="id">The identifier of the parent entity.</param>
        /// <returns>A task that represents the operation and contains a single parent entity query expression as a result.</returns>
        protected Task<Expression<Func<TParentEntity, Boolean>>> BuildSingleParentQueryExpressionAsync(TParentIdentifier id)
        {
            if (this.Overrides.BuildSingleParentQueryExpression != null)
            {
                return this.Overrides.BuildSingleParentQueryExpression(id);
            }

            async Task<Expression<Func<TParentEntity, Boolean>>> DefaultImplementation()
            {
                var property = await this.ControllerServices.MappingManager.GetModelIdentifierMapper<TParentIdentifier, TParentEntity>().GetModelIdentifierPropertyAsync();

                var parameter = Expression.Parameter(typeof(TParentEntity), "x");
                var equal = Expression.Equal(Expression.Property(parameter, property), Expression.Constant(id));

                var lambda = Expression.Lambda<Func<TParentEntity, Boolean>>(equal, parameter);
                return lambda;
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously queries a single entity from the entity store using specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>A task that represents the operation and contains queried entity as a result.</returns>
        protected Task<TEntity> QuerySingleEntityAsync(TIdentifier id)
        {
            if (this.Overrides.QuerySingleEntity != null)
            {
                return this.Overrides.QuerySingleEntity(id);
            }

            async Task<TEntity> DefaultImplementation()
            {
                var expression = await this.BuildSingleQueryExpressionAsync(id);
                var entity = await this.Store.Query().SingleOrDefaultAsync(expression);

                return entity;
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously builds the expression for single entity query.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>A task that represents the operation and contains a single entity query expression as a result.</returns>
        protected Task<Expression<Func<TEntity, Boolean>>> BuildSingleQueryExpressionAsync(TIdentifier id)
        {
            if (this.Overrides.BuildSingleQueryExpression != null)
            {
                return this.Overrides.BuildSingleQueryExpression(id);
            }

            async Task<Expression<Func<TEntity, Boolean>>> DefaultImplementation()
            {
                var property = await this.ControllerServices.MappingManager.GetModelIdentifierMapper<TIdentifier, TEntity>().GetModelIdentifierPropertyAsync();

                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var equal = Expression.Equal(Expression.Property(parameter, property), Expression.Constant(id));

                var lambda = Expression.Lambda<Func<TEntity, Boolean>>(equal, parameter);
                return lambda;
            }

            return DefaultImplementation();
        }

        /// <summary>
        /// Asynchronously gets all entity properties.
        /// </summary>
        /// <returns>A task that represents the operation and contains an array of entity properties as a result.</returns>
        protected virtual Task<String[]> GetAllEntityPropertiesAsync()
        {
            if (this.Overrides.GetAllEntityProperties != null)
            {
                return this.Overrides.GetAllEntityProperties();
            }

            var allProperties = typeof(TEntity).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return Task.FromResult(allProperties.Select(x => x.Name).ToArray());
        }

        /// <summary>
        /// Asynchronously gets an array of entity properties for which the current user has the specified permission.
        /// </summary>
        /// <param name="permission">The required permission.</param>
        /// <returns>A task that represents the operation and contains an array of entity properties as a result.</returns>
        protected virtual Task<String[]> GetAllowedEntityPropertiesAsync(Permission permission)
        {
            if (this.Overrides.GetAllowedEntityProperties != null)
            {
                return this.Overrides.GetAllowedEntityProperties(permission);
            }

            async Task<String[]> DefaultImplementation()
            {
                var properties = await this.GetAllEntityPropertiesAsync();

                var list = new List<String>();
                if (permission.Equals(EntityPermissions.EntityProperty.Read))
                {
                    foreach (var property in properties)
                    {
                        if (await this.PermissionsValidator.CanReadPropertyAsync(property))
                        {
                            list.Add(property);
                        }
                    }
                }
                else if (permission.Equals(EntityPermissions.EntityProperty.Initialize))
                {
                    foreach (var property in properties)
                    {
                        if (await this.PermissionsValidator.CanInitializePropertyAsync(property))
                        {
                            list.Add(property);
                        }
                    }
                }
                else if (permission.Equals(EntityPermissions.EntityProperty.Update))
                {
                    foreach (var property in properties)
                    {
                        if (await this.PermissionsValidator.CanUpdatePropertyAsync(property))
                        {
                            list.Add(property);
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException();
                }

                return list.ToArray();
            }

            return DefaultImplementation();
        }
    }
}
