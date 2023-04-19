using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;

namespace DevGuild.AspNetCore.Controllers.Mvc.ActionHandlers
{
    /// <summary>
    /// Represents base action handler.
    /// </summary>
    /// <typeparam name="TOverrides">The type of the overrides.</typeparam>
    public abstract class BaseActionHandler<TOverrides> : ActionHandler
        where TOverrides : BaseActionOverrides
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseActionHandler{TOverrides}"/> class.
        /// </summary>
        /// <param name="controller">The controller that contains new action handler.</param>
        protected BaseActionHandler(Controller controller)
            : base(controller)
        {
        }

        /// <summary>
        /// Gets the action overrides.
        /// </summary>
        /// <value>
        /// The action overrides.
        /// </value>
        public abstract TOverrides Overrides { get; }
    }
}
