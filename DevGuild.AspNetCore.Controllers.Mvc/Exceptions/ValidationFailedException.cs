using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevGuild.AspNetCore.Controllers.Mvc.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when model validation is failed.
    /// </summary>
    public class ValidationFailedException : Exception
    {
        private readonly ModelErrorsCollection errors = new ModelErrorsCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFailedException"/> class.
        /// </summary>
        public ValidationFailedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFailedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ValidationFailedException(String message)
            : base(message)
        {
            this.errors.AddModelError(String.Empty, message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFailedException"/> class.
        /// </summary>
        /// <param name="key">The model key.</param>
        /// <param name="message">The error message.</param>
        public ValidationFailedException(String key, String message)
            : base(message)
        {
            this.errors.AddModelError(key, message);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFailedException"/> class.
        /// </summary>
        /// <param name="errors">The errors messages.</param>
        public ValidationFailedException(IEnumerable<String> errors)
        {
            foreach (var error in errors)
            {
                this.errors.AddModelError(String.Empty, error);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFailedException"/> class.
        /// </summary>
        /// <param name="errors">The errors messages.</param>
        public ValidationFailedException(params String[] errors)
            : this((IEnumerable<String>)errors)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationFailedException"/> class.
        /// </summary>
        /// <param name="key">The model key.</param>
        /// <param name="errors">The errors messages.</param>
        public ValidationFailedException(String key, IEnumerable<String> errors)
        {
            foreach (var error in errors)
            {
                this.errors.AddModelError(key, error);
            }
        }

        /// <summary>
        /// Adds the errors from this exception to the specified model state.
        /// </summary>
        /// <param name="modelState">The model state.</param>
        public void AddErrorsToModelState(ModelStateDictionary modelState)
        {
            this.errors.AddToModelState(modelState);
        }
    }
}
