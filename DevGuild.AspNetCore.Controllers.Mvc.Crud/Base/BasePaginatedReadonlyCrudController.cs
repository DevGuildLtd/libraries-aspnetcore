using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Controllers.Mvc.Crud.ActionHandlers;
using DevGuild.AspNetCore.ObjectModel;
using DevGuild.AspNetCore.Services.Data;
using DevGuild.AspNetCore.Services.Permissions.Annotations;
using Microsoft.AspNetCore.Mvc;

namespace DevGuild.AspNetCore.Controllers.Mvc.Crud.Base
{
    public abstract class BasePaginatedReadonlyCrudController<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel, TDetailsModel> : Controller
        where TEntity : class, new()
        where TIndexViewModel : class, IEntityPaginatedIndexModel<TIndexItemModel>, new()
        where TDetailsModel : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePaginatedFullCrudController{TIdentifier, TEntity, TIndexViewModel, TIndexItemModel, TDetailsModel, TCreateModel, TEditModel, TDeleteModel}"/> class.
        /// </summary>
        /// <param name="controllerServices">The controller services.</param>
        protected BasePaginatedReadonlyCrudController(IEntityControllerServices controllerServices)
        {
            this.ControllerServices = controllerServices;
            this.PermissionsValidator = this.GetEntityPermissionsValidator();

            this.IndexHandler = new BasicCrudPaginatedIndexActionHandler<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel>(this, controllerServices, this.PermissionsValidator);
            this.DetailsHandler = new BasicCrudDetailsActionHandler<TIdentifier, TEntity, TDetailsModel>(this, controllerServices, this.PermissionsValidator);
        }

        /// <summary>
        /// Gets the Index action handler.
        /// </summary>
        /// <value>
        /// The Index action handler.
        /// </value>
        protected BasicCrudPaginatedIndexActionHandler<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel> IndexHandler { get; }

        /// <summary>
        /// Gets the Details action handler.
        /// </summary>
        /// <value>
        /// The Details action handler.
        /// </value>
        protected BasicCrudDetailsActionHandler<TIdentifier, TEntity, TDetailsModel> DetailsHandler { get; }

        /// <summary>
        /// Gets the controller services.
        /// </summary>
        /// <value>
        /// The controller services.
        /// </value>
        protected IEntityControllerServices ControllerServices { get; }

        /// <summary>
        /// Gets the repository service.
        /// </summary>
        /// <value>
        /// The repository service.
        /// </value>
        protected IRepository Repository => this.ControllerServices.Repository;

        /// <summary>
        /// Gets the permissions validator.
        /// </summary>
        /// <value>
        /// The permissions validator.
        /// </value>
        protected IEntityPermissionsValidator<TEntity> PermissionsValidator { get; }

        /// <summary>
        /// Handles the GET request for the Index action.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> that renders Index action page.</returns>
        public virtual Task<IActionResult> Index(Int32? page) => this.IndexHandler.Index(page);

        /// <summary>
        /// Handles the GET request for the Details action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="ActionResult"/> that renders Details action page.</returns>
        public virtual Task<IActionResult> Details(TIdentifier id) => this.DetailsHandler.Details(id);

        /// <summary>
        /// Creates the dependent entity permissions validator.
        /// </summary>
        /// <returns>A created permissions validator.</returns>
        protected virtual IEntityPermissionsValidator<TEntity> GetEntityPermissionsValidator()
        {
            return new DefaultEntityPermissionsValidator<TEntity>(
                permissionsHub: this.ControllerServices.PermissionsHub,
                typeManagerPath: PermissionsManagerAttribute.GetAnnotatedPermissionsManager(this.GetType(), "Type"),
                entityManagerPath: PermissionsManagerAttribute.GetAnnotatedPermissionsManager(this.GetType(), "Entity"),
                propertyManagerPath: PermissionsManagerAttribute.GetAnnotatedPermissionsManager(this.GetType(), "Property"));
        }
    }
}
