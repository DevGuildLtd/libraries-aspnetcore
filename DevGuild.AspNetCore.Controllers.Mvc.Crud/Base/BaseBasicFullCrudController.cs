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
    /// <summary>
    /// Represents base controller that provides overridable basic CRUD actions implementations as a number of action handlers.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TIndexViewModel">The type of the index view model.</typeparam>
    /// <typeparam name="TIndexItemModel">The type of the index item model.</typeparam>
    /// <typeparam name="TDetailsModel">The type of the details model.</typeparam>
    /// <typeparam name="TCreateModel">The type of the create model.</typeparam>
    /// <typeparam name="TEditModel">The type of the edit model.</typeparam>
    /// <typeparam name="TDeleteModel">The type of the delete model.</typeparam>
    public abstract class BaseBasicFullCrudController<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel, TDetailsModel, TCreateModel, TEditModel, TDeleteModel> : Controller
        where TEntity : class, new()
        where TIndexViewModel : class, IEntityIndexModel<TIndexItemModel>, new()
        where TDetailsModel : class
        where TCreateModel : class, new()
        where TEditModel : class, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBasicFullCrudController{TIdentifier, TEntity, TIndexViewModel, TIndexItemModel, TDetailsModel, TCreateModel, TEditModel, TDeleteModel}"/> class.
        /// </summary>
        /// <param name="controllerServices">The controller services.</param>
        protected BaseBasicFullCrudController(IEntityControllerServices controllerServices)
        {
            this.ControllerServices = controllerServices;
            this.PermissionsValidator = this.GetEntityPermissionsValidator();

            this.IndexHandler = new BasicCrudIndexActionHandler<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel>(this, controllerServices, this.PermissionsValidator);
            this.DetailsHandler = new BasicCrudDetailsActionHandler<TIdentifier, TEntity, TDetailsModel>(this, controllerServices, this.PermissionsValidator);
            this.CreateHandler = new BasicCrudCreateActionHandler<TIdentifier, TEntity, TCreateModel>(this, controllerServices, this.PermissionsValidator);
            this.EditHandler = new BasicCrudEditActionHandler<TIdentifier, TEntity, TDetailsModel, TEditModel>(this, controllerServices, this.PermissionsValidator);
            this.DeleteHandler = new BasicCrudDeleteActionHandler<TIdentifier, TEntity, TDeleteModel>(this, controllerServices, this.PermissionsValidator);
        }

        /// <summary>
        /// Gets the Index action handler.
        /// </summary>
        /// <value>
        /// The Index action handler.
        /// </value>
        protected BasicCrudIndexActionHandler<TIdentifier, TEntity, TIndexViewModel, TIndexItemModel> IndexHandler { get; }

        /// <summary>
        /// Gets the Details action handler.
        /// </summary>
        /// <value>
        /// The Details action handler.
        /// </value>
        protected BasicCrudDetailsActionHandler<TIdentifier, TEntity, TDetailsModel> DetailsHandler { get; }

        /// <summary>
        /// Gets the Create action handler.
        /// </summary>
        /// <value>
        /// The Create action handler.
        /// </value>
        protected BasicCrudCreateActionHandler<TIdentifier, TEntity, TCreateModel> CreateHandler { get; }

        /// <summary>
        /// Gets the Edit action handler.
        /// </summary>
        /// <value>
        /// The Edit action handler.
        /// </value>
        protected BasicCrudEditActionHandler<TIdentifier, TEntity, TDetailsModel, TEditModel> EditHandler { get; }

        /// <summary>
        /// Gets the Delete action handler.
        /// </summary>
        /// <value>
        /// The Delete action handler.
        /// </value>
        protected BasicCrudDeleteActionHandler<TIdentifier, TEntity, TDeleteModel> DeleteHandler { get; }

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
        public virtual Task<IActionResult> Index() => this.IndexHandler.Index();

        /// <summary>
        /// Handles the GET request for the Details action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="ActionResult"/> that renders Details action page.</returns>
        public virtual Task<IActionResult> Details(TIdentifier id) => this.DetailsHandler.Details(id);

        /// <summary>
        /// Handles the GET request for the Create action.
        /// </summary>
        /// <returns>An <see cref="ActionResult"/> that renders Create action form.</returns>
        public virtual Task<IActionResult> Create() => this.CreateHandler.Create();

        /// <summary>
        /// Handles the POST request for the Create action.
        /// </summary>
        /// <param name="model">The create model.</param>
        /// <returns>An <see cref="ActionResult"/> that redirects to another page on success or renders Create action form again on failure.</returns>
        /// <remarks>This method requires a valid anti-forgery token to be submitted.</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual Task<IActionResult> Create(TCreateModel model) => this.CreateHandler.Create(model);

        /// <summary>
        /// Handles the GET request for the Edit action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="ActionResult"/> that renders Edit action form.</returns>
        public virtual Task<IActionResult> Edit(TIdentifier id) => this.EditHandler.Edit(id);

        /// <summary>
        /// Handles the POST request for the Edit action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <param name="model">The edit model.</param>
        /// <returns>An <see cref="ActionResult"/> that redirects to another page on success or renders Edit action form again on failure.</returns>
        /// <remarks>This method requires a valid anti-forgery token to be submitted.</remarks>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual Task<IActionResult> Edit(TIdentifier id, TEditModel model) => this.EditHandler.Edit(id, model);

        /// <summary>
        /// Handles the GET request for the Delete action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="ActionResult"/> that renders Delete action form.</returns>
        public virtual Task<IActionResult> Delete(TIdentifier id) => this.DeleteHandler.Delete(id);

        /// <summary>
        /// Handles the POST request for the Delete action.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>An <see cref="ActionResult"/> that redirects to another page on success or renders Delete action form again on failure.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual Task<IActionResult> DeleteConfirmed(TIdentifier id) => this.DeleteHandler.DeleteConfirmed(id);

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
