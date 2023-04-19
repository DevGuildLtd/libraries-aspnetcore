using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.ObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents dependent hierarchical Create action handler overrides.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TParentIdentifier">The type of the parent identifier.</typeparam>
    /// <typeparam name="TParentEntity">The type of the parent entity.</typeparam>
    /// <typeparam name="TCreateModel">The type of the create model.</typeparam>
    public class BasicHierarchicalCrudDependentCreateActionOverrides<TIdentifier, TEntity, TParentIdentifier, TParentEntity, TCreateModel> : BaseCrudDependentActionOverrides<TIdentifier, TEntity, TParentIdentifier, TParentEntity>
        where TIdentifier : struct
        where TEntity : class, IHierarchicalEntity<TIdentifier, TEntity>, new()
        where TParentEntity : class
        where TCreateModel : class, IHierarchicalCreateModel<TEntity>, new()
    {
        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.DemandCanCreateAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.DemandCanCreateAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentEntity, Task> DemandCanCreate { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.InitializeCreateModelAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.InitializeCreateModelAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentEntity, TEntity, TCreateModel, Boolean, Task> InitializeCreateModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.InitializeNewEntityAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.InitializeNewEntityAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentEntity, TEntity, TEntity, TCreateModel, Task> InitializeNewEntity { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.InitializeNewEntityParentAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.InitializeNewEntityParentAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentEntity, TEntity, TEntity, TCreateModel, Task> InitializeNewEntityParent { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.InitializeNewEntityConcurrencyAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.InitializeNewEntityConcurrencyAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentEntity, TEntity, TEntity, Task> InitializeNewEntityConcurrency { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.ValidateCreateModelAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.ValidateCreateModelAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentEntity, TEntity, TCreateModel, Task<Boolean>> ValidateCreateModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.BeforeEntityCreatedAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.BeforeEntityCreatedAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentEntity, TEntity, TEntity, TCreateModel, Dictionary<String, Object>, Task> BeforeEntityCreated { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.AfterEntityCreatedAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.AfterEntityCreatedAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentEntity, TEntity, TEntity, TCreateModel, Dictionary<String, Object>, Task> AfterEntityCreated { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.GetCreateViewResultAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.GetCreateViewResultAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentEntity, TEntity, TCreateModel, Task<IActionResult>> GetCreateViewResult { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.GetCreateSuccessResultAsync"/> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudDependentCreateActionHandler{TIdentifier,TEntity,TParentIdentifier,TParentEntity,TCreateModel}.GetCreateSuccessResultAsync"/> method of the related action handler.
        /// </value>
        public Func<TParentEntity, TEntity, TEntity, TCreateModel, Dictionary<String, Object>, Task<IActionResult>> GetCreateSuccessResult { get; set; }
    }
}
