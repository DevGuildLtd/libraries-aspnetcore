using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace DevGuild.AspNetCore.Controllers.Mvc.ActionHandlers
{
    public abstract class ActionHandler
    {
        protected ActionHandler(Controller controller)
        {
            this.Controller = controller;
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        protected Controller Controller { get; }

        /// <summary>
        /// Gets the model state dictionary.
        /// </summary>
        /// <value>
        /// The model state dictionary.
        /// </value>
        protected ModelStateDictionary ModelState => this.Controller.ModelState;

        /// <summary>
        /// Creates a <see cref="ViewResult"/> object using the view name and model that renders a view.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns>The view result.</returns>
        protected ViewResult View(String viewName, Object model) => this.Controller.View(viewName, model);

        /// <summary>
        /// Creates a <see cref="ViewResult"/> object using the view name that renders a view.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <returns>The view result.</returns>
        protected ViewResult View(String viewName) => this.Controller.View(viewName);

        /// <summary>
        /// Creates a <see cref="ViewResult"/> object using the model that renders a view.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The view result.</returns>
        protected ViewResult View(Object model) => this.Controller.View(model);

        /// <summary>
        /// Creates a <see cref="ViewResult"/> object that renders a view.
        /// </summary>
        /// <returns>The view result.</returns>
        protected ViewResult View() => this.Controller.View();

        /// <summary>
        /// Creates a <see cref="JsonResult"/> object that serializes the specified object to JSON format using the content type, content encoding, and the JSON request behavior.
        /// </summary>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <param name="serializerSettings">The JsonSerializerSettings used by a formatter.</param>
        /// <returns>The result object that serializes the specified object to JSON format.</returns>
        protected JsonResult Json(Object data, JsonSerializerSettings serializerSettings) => this.Controller.Json(data, serializerSettings);

        /// <summary>
        /// Creates a <see cref="JsonResult"/> object that serializes the specified object to JSON format using the content type and the JSON request behavior.
        /// </summary>
        /// <param name="data">The JavaScript object graph to serialize.</param>
        /// <param name="contentType">The content type (MIME type).</param>
        /// <param name="behavior">The JSON request behavior.</param>
        /// <returns>The result object that serializes the specified object to JSON format.</returns>
        protected JsonResult Json(Object data) => this.Controller.Json(data);

        /// <summary>
        /// Redirects to the specified action using the action name, controller name and route values.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The redirect result object.</returns>
        protected RedirectToActionResult RedirectToAction([AspMvcAction] String actionName, [AspMvcController] String controllerName, Object routeValues) => this.Controller.RedirectToAction(actionName, controllerName, routeValues);

        /// <summary>
        /// Redirects to the specified action using the action name and controller name.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns>The redirect result object.</returns>
        protected RedirectToActionResult RedirectToAction([AspMvcAction] String actionName, [AspMvcController] String controllerName) => this.Controller.RedirectToAction(actionName, controllerName);

        /// <summary>
        /// Redirects to the specified action using the action name and route values.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The redirect result object.</returns>
        protected RedirectToActionResult RedirectToAction([AspMvcAction] String actionName, Object routeValues) => this.Controller.RedirectToAction(actionName, routeValues);

        /// <summary>
        /// Redirects to the specified action using the action name.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <returns>The redirect result object.</returns>

        protected RedirectToActionResult RedirectToAction([AspMvcAction] String actionName) => this.Controller.RedirectToAction(actionName);

        /// <summary>
        /// Returns an instance of the <see cref="NotFoundResult"/> class.
        /// </summary>
        /// <returns>An instance of the <see cref="NotFoundResult"/> class.</returns>
        protected NotFoundResult NotFound() => this.Controller.NotFound();

        protected StatusCodeResult StatusCode(Int32 statusCode) => this.Controller.StatusCode(statusCode);

        protected ObjectResult StatusCode(Int32 statusCode, Object value) => this.Controller.StatusCode(statusCode, value);
    }
}
