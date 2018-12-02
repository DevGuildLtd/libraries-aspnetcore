﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers
{
    /// <summary>
    /// Represents hierarchical Create action handler overrides.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TCreateModel">The type of the create model.</typeparam>
    public class BasicHierarchicalCrudCreateActionOverrides<TIdentifier, TEntity, TCreateModel> : BaseCrudActionOverrides<TIdentifier, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.InitializeCreateModelAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.InitializeCreateModelAsync"/> method of the related action handler.
        /// </value>
        public Func<TCreateModel, Boolean, Task> InitializeCreateModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.InitializeNewEntityAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.InitializeNewEntityAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TCreateModel, Task> InitializeNewEntity { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.InitializeNewEntityConcurrencyAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.InitializeNewEntityConcurrencyAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, Task> InitializeNewEntityConcurrency { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.ValidateCreateModelAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.ValidateCreateModelAsync"/> method of the related action handler.
        /// </value>
        public Func<TCreateModel, Task<Boolean>> ValidateCreateModel { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.BeforeEntityCreatedAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.BeforeEntityCreatedAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TCreateModel, Dictionary<String, Object>, Task> BeforeEntityCreated { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.AfterEntityCreatedAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.AfterEntityCreatedAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TCreateModel, Dictionary<String, Object>, Task> AfterEntityCreated { get; set; }

        /// <summary>
        /// Gets or sets the override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.GetCreateSuccessResultAsync" /> method of the related action handler.
        /// </summary>
        /// <value>
        /// The override implementation of the <see cref="BasicHierarchicalCrudCreateActionHandler{TIdentifier,TEntity,TCreateModel}.GetCreateSuccessResultAsync"/> method of the related action handler.
        /// </value>
        public Func<TEntity, TCreateModel, Dictionary<String, Object>, Task<ActionResult>> GetCreateSuccessResult { get; set; }
    }
}
