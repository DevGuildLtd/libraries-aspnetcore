using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevGuild.AspNetCore.Controllers.Mvc.Exceptions
{
    /// <summary>
    /// Represents a collection of model errors.
    /// </summary>
    public class ModelErrorsCollection
    {
        private readonly Dictionary<String, List<String>> modelErrors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelErrorsCollection"/> class.
        /// </summary>
        public ModelErrorsCollection()
        {
            this.modelErrors = new Dictionary<String, List<String>>();
        }

        /// <summary>
        /// Adds the model error to the collection.
        /// </summary>
        /// <param name="key">The model key.</param>
        /// <param name="error">The error message.</param>
        public void AddModelError(String key, String error)
        {
            if (!this.modelErrors.TryGetValue(key, out var list))
            {
                list = new List<String>();
                this.modelErrors.Add(key, list);
            }

            list.Add(error);
        }

        /// <summary>
        /// Adds the errors of the collection to the specified model state.
        /// </summary>
        /// <param name="modelState">The model state.</param>
        public void AddToModelState(ModelStateDictionary modelState)
        {
            foreach (var errorList in this.modelErrors)
            {
                foreach (var error in errorList.Value)
                {
                    modelState.AddModelError(errorList.Key, error);
                }
            }
        }
    }
}
