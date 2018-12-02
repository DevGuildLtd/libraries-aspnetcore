using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevGuild.AspNetCore.Services.ModelMapping
{
    /// <summary>
    /// Defines interface for view model identifier mapper.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    public interface IViewModelIdentifierMapper<TIdentifier, in TViewModel>
        where TViewModel : class
    {
        /// <summary>
        /// Asynchronously gets the view model identifier property.
        /// </summary>
        /// <returns>A task that represents the operation.</returns>
        Task<PropertyInfo> GetViewModelIdentifierPropertyAsync();

        /// <summary>
        /// Asynchronously gets the view model identifier.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>A task that represents the operation.</returns>
        Task<TIdentifier> GetViewModelIdentifierAsync(TViewModel model);
    }
}
