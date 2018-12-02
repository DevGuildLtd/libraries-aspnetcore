using System;
using System.Collections.Generic;
using System.Text;

namespace DevGuild.AspNetCore.Services.ModelMapping
{
    /// <summary>
    /// Defines interface of the view model mapping manager.
    /// </summary>
    public interface IViewModelMappingManager
    {
        /// <summary>
        /// Gets the model mapper.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <returns>A model mapper.</returns>
        IViewModelMapper<TModel, TViewModel> GetModelMapper<TModel, TViewModel>();

        /// <summary>
        /// Gets the model identifier mapper.
        /// </summary>
        /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <returns>A model identifier mapper.</returns>
        IModelIdentifierMapper<TIdentifier, TModel> GetModelIdentifierMapper<TIdentifier, TModel>()
            where TModel : class;

        /// <summary>
        /// Gets the view model identifier mapper.
        /// </summary>
        /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
        /// <typeparam name="TViewModel">The type of the view model.</typeparam>
        /// <returns>A view model identifier mapper.</returns>
        IViewModelIdentifierMapper<TIdentifier, TViewModel> GetViewModelIdentifierMapper<TIdentifier, TViewModel>()
            where TViewModel : class;
    }
}
